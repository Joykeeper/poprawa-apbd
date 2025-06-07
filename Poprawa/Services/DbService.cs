namespace Poprawa.Services;
using Microsoft.Data.SqlClient;

public class DbService : IDbService
{
    private readonly string _connectionString = "Data Source=db-mssql;Initial Catalog=PoprawaDb;Integrated Security=True;Trust Server Certificate=True";
    
    // public async Task<List<Customer>> GetCustomersAsync()
    // {
    //     List<Customer> customers = new List<Customer>();
    //     string query = "SELECT CustomerId, FirstName, LastName, Email FROM Customers";
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
 //             INSERT INTO Customers (FirstName, LastName, Email)
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
    //     string query = @"UPDATE Customers
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
    //     string query = "DELETE FROM Customers WHERE CustomerId = @CustomerId";
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
}