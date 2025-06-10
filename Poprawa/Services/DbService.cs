namespace Poprawa.Services;
using Microsoft.Data.SqlClient;

public class DbService : IDbService
{
    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PoprawaDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    
    // check if record exists in table
    // public async Task<bool> DoesAnimalExists(int id)
    // {
    //     
    //     string query = "SELECT * FROM Animal WHERE Id = @Id";
    //     
    //     using (SqlConnection connection = new SqlConnection(_connectionString))
    //     using (SqlCommand command = new SqlCommand(query, connection))
    //     {
    //         command.Parameters.AddWithValue("@Id", id);
    //         
    //         await connection.OpenAsync();
    //         
    //         var result = await command.ExecuteScalarAsync();
    //         return result != null;
    //     }
    // }
    
    
    // public async Task<List<Customer>> GetCustomersAsync()
    // {
    //     List<Customer> customers = new List<Customer>();
    //     string query = "SELECT CustomerId, FirstName, LastName, Email FROM Customer";
    //
    //     using (SqlConnection connection = new SqlConnection(_connectionString))
    //     using (SqlCommand command = new SqlCommand(query, connection))
    //     {
    //         // Otwórz połączenie asynchronicznie
    //         await connection.OpenAsync();
    //         using (SqlDataReader reader = await command.ExecuteReaderAsync())
    //         {
    //             // Odczytaj wyniki asynchronicznie
    //             while (await reader.ReadAsync())
    //             {
    //                 customers.Add(new Customer
    //                 {
    //                     CustomerId = reader.GetInt32(0),
    //                     FirstName = reader.GetString(1),
    //                     LastName = reader.GetString(2),
    //                     Email = reader.GetString(3)
    //                 });
    //             }
    //         }
    //     }
    // }

    // Inserting and getting id of inserted elem
 //    public async Task<int> AddCustomerAsync(Customer customer)
 //    {
 //        string query = @"
 //             INSERT INTO Customer (FirstName, LastName, Email)
 //             VALUES (@FirstName, @LastName, @Email);
 //             SELECT SCOPE_IDENTITY();";
 //
 //        using (SqlConnection connection = new SqlConnection(_connectionString))
 //        using (SqlCommand command = new SqlCommand(query, connection))
 //        {
 //            command.Parameters.AddWithValue("@FirstName", customer.FirstName);
 //            command.Parameters.AddWithValue("@LastName", customer.LastName);
 //            command.Parameters.AddWithValue("@Email", customer.Email);
 //
 //            await connection.OpenAsync();
 //
 //            // ExecuteScalarAsync zwraca pierwszą kolumnę pierwszego wiersza
 //            var result = await command.ExecuteScalarAsync();
 //            return Convert.ToInt32(result);
 //        }
 //    }

    
    // Updating
    // public async Task<bool> UpdateCustomerAsync(Customer customer)
    // {
    //     string query = @"UPDATE Customer
    //                     SET FirstName = @FirstName,
    //                         LastName = @LastName,
    //                         Email = @Email
    //                     WHERE CustomerId = @CustomerId";
    //
    //     using (SqlConnection connection = new SqlConnection(_connectionString))
    //     using (SqlCommand command = new SqlCommand(query, connection))
    //     {
    //         command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
    //         command.Parameters.AddWithValue("@FirstName", customer.FirstName);
    //         command.Parameters.AddWithValue("@LastName", customer.LastName);
    //         command.Parameters.AddWithValue("@Email", customer.Email);
    //
    //         await connection.OpenAsync();
    //
    //         int rowsAffected = await command.ExecuteNonQueryAsync();
    //         return rowsAffected > 0;
    //     }
    // }
    
    // Deleting
    // public async Task<bool> DeleteCustomerAsync(int customerId)
    // {
    //     string query = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
    //
    //     using (SqlConnection connection = new SqlConnection(_connectionString))
    //     using (SqlCommand command = new SqlCommand(query, connection))
    //     {
    //         command.Parameters.AddWithValue("@CustomerId", customerId);
    //
    //         await connection.OpenAsync();
    //
    //         int rowsAffected = await command.ExecuteNonQueryAsync();
    //         return rowsAffected > 0;
    //     }
    // }
    
    
    // procedura składowana
    // public async Task<List<Customer>> GetCustomersByCountryAsync(string country)
    // {
    //     List<Customer> customers = new List<Customer>();
    //
    //     using (SqlConnection connection = new SqlConnection(_connectionString))
    //     using (SqlCommand command = new SqlCommand("GetCustomersByCountry", connection))
    //     {
    //         // Określ, że wykonujemy procedurę składowaną
    //         command.CommandType = CommandType.StoredProcedure;
    //
    //         // Dodaj parametry
    //         command.Parameters.AddWithValue("@Country", country);
    //
    //         await connection.OpenAsync();
    //
    //         using (SqlDataReader reader = await command.ExecuteReaderAsync())
    //         {
    //             while (await reader.ReadAsync())
    //             {
    //                 customers.Add(new Customer
    //                 {
    //                     CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
    //                     FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
    //                     LastName = reader.GetString(reader.GetOrdinal("LastName")),
    //                     Email = reader.GetString(reader.GetOrdinal("Email"))
    //                 });
    //             }
    //         }
    //     }
    //
    //     return customers;
    // }
    
    
    // one transaction
    // public async Task AddAnimal(NewAnimalDto newAnimal)
    // {
    //     
    //     string query = @"INSERT INTO Animal (Name, Type, AdmissionDate, Owner_Id)
    //                     VALUES (@Name, @Type, @AdmissionDate, @OwnerId);
    //                     SELECT SCOPE_IDENTITY()";
    //     
    //     string query2 = @"INSERT INTO Procedure_Animal (Procedure_ID, Animal_ID, Date)
    //                     VALUES (@ProcedureId, @AnimalId, @Date);";
    //
    //     
    //     using (SqlConnection connection = new SqlConnection(_connectionString))
    //     using (SqlCommand command = new SqlCommand(query, connection))
    //     {
    //         await connection.OpenAsync();                                                        <- Open connection
    //         
    //         var transaction = await connection.BeginTransactionAsync();                          <- start transaction
    //         command.Transaction = transaction as SqlTransaction;
    //         
    //         try
    //         {
    //             if (await DoesOwnerExists(newAnimal.OwnerId) == false)
    //             {
    //                 throw new NotFoundException("Owner not found");
    //             }
    //             
    //             command.Parameters.AddWithValue("@Name", newAnimal.Name);
    //             command.Parameters.AddWithValue("@Type", newAnimal.Type);
    //             command.Parameters.AddWithValue("@AdmissionDate", newAnimal.AdmissionDate);
    //             command.Parameters.AddWithValue("@OwnerId", newAnimal.OwnerId);
    //             
    //             // first query
    //             var newAnimalId = Convert.ToInt32(await command.ExecuteScalarAsync());          <- executing query1
    //
    //             command.CommandText = query2;                                                   <- change query
    //             //second query
    //             foreach (var procedure in newAnimal.Procedures)
    //             {
    //                 command.Parameters.Clear();                                                 <- clear parameters
    //                 if (await DoesProcedureExists(procedure.ProcedureId) == false)
    //                 {
    //                     throw new NotFoundException("Procedure not found");
    //                 }
    //                 
    //                 command.Parameters.AddWithValue("@ProcedureId", procedure.ProcedureId);     <- set parameters
    //
    //                 
    //                 await command.ExecuteScalarAsync();                                         <- executing query2
    //             }
    //
    //             await transaction.CommitAsync();                                                <- commit changes
    //         }
    //         catch (Exception)
    //         {
    //             await transaction.RollbackAsync();                                              <- rollback changes
    //             throw;
    //         }
    //     }
    //     
    // }
    
    // Selecting an object and list of objects from association table using one command object
    // public async Task<AnimalDto> GetAnimalById(int id)
    // {
    //     if (await DoesAnimalExists(id) == false)
    //     {
    //         throw new NotFoundException("Animal with provided id not found");
    //     }
    //     
    //     
    //     string query = @"SELECT A.ID, A.Name, A.Type, A.AdmissionDate, O.ID, O.FirstName, O.LastName FROM Animal AS A
    //         JOIN Owner AS O ON Owner_ID = A.Owner_ID WHERE A.Id = @Id";
    //     
    //     string query2 = @"SELECT Name, Description, Date FROM [Procedure] AS P
    //         JOIN Procedure_Animal AS PA ON ID = PA.Procedure_ID WHERE PA.Animal_ID = @Id";
    //     
    //     var animal = new AnimalDto();
    //     
    //     
    //     using (SqlConnection connection = new SqlConnection(_connectionString))
    //     using (SqlCommand command = new SqlCommand(query, connection))
    //     {
    //         command.Parameters.AddWithValue("@Id", id);
    //         
    //         await connection.OpenAsync();
    //         
    //         using (SqlDataReader reader = await command.ExecuteReaderAsync())
    //         {
    //             // Odczytaj wyniki asynchronicznie
    //             while (await reader.ReadAsync())
    //             {
    //                 animal.Id = reader.GetInt32(0);
    //                 animal.Name = reader.GetString(1);
    //                 animal.Type = reader.GetString(2);
    //                 animal.AdmissionDate = reader.GetDateTime(3);
    //                 animal.Owner = new OwnerDto()
    //                 {
    //                     Id = reader.GetInt32(4),
    //                     FirstName = reader.GetString(5),
    //                     LastName = reader.GetString(6)
    //                 };
    //             }
    //         }
    //         
    //         command.CommandText = query2;
    //         command.Parameters.Clear();
    //         command.Parameters.AddWithValue("@Id", id);
    //         
    //         var procedures = new List<ProcedureDto>();
    //         
    //         
    //         using (SqlDataReader reader = await command.ExecuteReaderAsync())
    //         {
    //             // Odczytaj wyniki asynchronicznie
    //             while (await reader.ReadAsync())
    //             {
    //                 procedures.Add(new ProcedureDto()
    //                 {
    //                     Name = reader.GetString(0),
    //                     Description = reader.GetString(1),
    //                     Date = reader.GetDateTime(2),
    //                 });
    //             }
    //         }
    //         
    //         
    //         animal.Procedures = procedures;
    //
    //         return animal;
    //     }
    // }
}