using Kol2Preparation.Exceptions;
using Microsoft.Data.SqlClient;
using Poprawa.DTOs;

namespace Poprawa.Services;

public class DbService : IDbService
{
    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PoprawaDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    
    public async Task<bool> DoesProjectExists(int id)
    {
        
        string query = "SELECT * FROM Preservation_Project WHERE ProjectId = @Id";
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Id", id);
            
            await connection.OpenAsync();
            
            var result = await command.ExecuteScalarAsync();
            return result != null;
        }
    }
    
    public async Task<bool> DoesArtifactExists(int id)
    {
        
        string query = "SELECT * FROM Artifact WHERE ArtifactId = @Id";
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Id", id);
            
            await connection.OpenAsync();
            
            var result = await command.ExecuteScalarAsync();
            return result != null;
        }
    }
    
    public async Task<bool> DoesInstitutionExists(int id)
    {
        
        string query = "SELECT * FROM Institution WHERE InstitutionId = @Id";
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Id", id);
            
            await connection.OpenAsync();
            
            var result = await command.ExecuteScalarAsync();
            return result != null;
        }
    }
    
    public async Task<ProjectDto> GetProjectInfo(int id)
    {
        if (await DoesProjectExists(id) == false)
        {
            throw new NotFoundException("Project with provided id not found");
        }


        string query = @"SELECT P.ProjectId, P.Objective, P.StartDate, P.EndDate, A.Name, A.OriginDate, 
        I.InstitutionId, I.Name, I.FoundedYear 
        FROM Preservation_Project AS P
        JOIN Artifact AS A ON P.ArtifactId = A.ArtifactId
        JOIN Institution AS I ON A.InstitutionId = I.InstitutionId
        WHERE P.ProjectId = @Id;";
    
        string query2 = @"SELECT S.FirstName, S.LastName, S.HireDate, SA.Role FROM Staff_Assignment AS SA
        JOIN Staff AS S ON S.StaffId = SA.StaffId WHERE SA.ProjectId = @Id";
    
        var project = new ProjectDto();
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    project.ProjectId = reader.GetInt32(0);
                    project.Objective = reader.GetString(1);
                    project.StartDate = reader.GetDateTime(2);

                    if (!reader.IsDBNull(3))
                    {
                        project.EndDate = reader.GetDateTime(3);
                    }
                    project.Artifact = new ArtifactDto()
                    {
                        Name = reader.GetString(4),
                        OriginDate = reader.GetDateTime(5),
                        Institution = new InstitutionDto()
                        {
                            InstitutionId = reader.GetInt32(6),
                            Name = reader.GetString(7),
                            FoundedYear = reader.GetInt32(8)
                        }
                    };
                }
            }

            command.CommandText = query2;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Id", id);

            var staff = new List<StaffAssignmentDto>();


            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    staff.Add(new StaffAssignmentDto()
                    {
                        FirstName = reader.GetString(0),
                        LastName = reader.GetString(1),
                        HireDate = reader.GetDateTime(2),
                        Role = reader.GetString(3),
                    });
                }
            }


            project.StaffAssignments = staff;

            return project;
        }
        
    }

    public async Task AddArtifact(NewArtifactProjectDto artifactProject)
    {
            string query = @"INSERT INTO Artifact (ArtifactId, Name, OriginDate, InstitutionId)
                    VALUES (@ArtifactId, @Name, @OriginDate, @InstitutionId);
                    SELECT SCOPE_IDENTITY()";
            
            string query2 = @"INSERT INTO Preservation_Project (ArtifactId, ProjectId, Objective, StartDate, EndDate)
                    VALUES (@ArtifactId, @ProjectId, @Objective, @StartDate, @EndDate);";
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                await connection.OpenAsync();

                var transaction = await connection.BeginTransactionAsync();
                command.Transaction = transaction as SqlTransaction;

                try
                {
                    if (await DoesArtifactExists(artifactProject.Artifact.ArtifactId))
                    {
                        throw new ConflictException("Artifact already exists");
                    }
                    if (await DoesProjectExists(artifactProject.Project.ProjectId))
                    {
                        throw new ConflictException("Project already exists");
                    }
                    if (await DoesInstitutionExists(artifactProject.Artifact.InstitutionId) == false)
                    {
                        throw new NotFoundException("Institution not found");
                    }

                    command.Parameters.AddWithValue("@ArtifactId", artifactProject.Artifact.ArtifactId);
                    command.Parameters.AddWithValue("@Name", artifactProject.Artifact.Name);
                    command.Parameters.AddWithValue("@OriginDate", artifactProject.Artifact.OriginDate);
                    command.Parameters.AddWithValue("@InstitutionId", artifactProject.Artifact.InstitutionId);

                    // first query
                    await command.ExecuteScalarAsync();
                    
                    
                    command.CommandText = query2;
                    
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@ArtifactId", artifactProject.Artifact.ArtifactId);
                    command.Parameters.AddWithValue("@ProjectId", artifactProject.Project.ProjectId);
                    command.Parameters.AddWithValue("@Objective", artifactProject.Project.Objective);
                    command.Parameters.AddWithValue("@StartDate", artifactProject.Project.StartDate);
                    if (artifactProject.Project.EndDate != null)
                    {
                        command.Parameters.AddWithValue("@EndDate", artifactProject.Project.EndDate);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@EndDate", DBNull.Value);
                    }
                    
                    //second query
                    await command.ExecuteScalarAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

    }
}