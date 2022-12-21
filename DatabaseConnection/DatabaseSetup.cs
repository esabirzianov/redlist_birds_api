using Microsoft.AspNetCore.Mvc;
using Npgsql;
namespace redlist_birds_api.DatabaseConnection;

public class DatabaseSetup
{
    public static string connectionString = "Host = localhost; Username = postgres; Password = Emulka7823; Database = dbforbirds";
    
    // Connection string to Database
    public static async Task OpenConnection(string conString, string comName, int howMany)
    {
        await using var conn = new NpgsqlConnection(conString);
        await using var cmd = new NpgsqlCommand("INSERT INTO  ebird_data (common_name, how_many_observed) VALUES (@comName, @howMany) ", conn) 
        {
            Parameters =  
            {
                new ("comName", comName),
                new ("howMany", howMany)
            }
        };
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                await conn.CloseAsync();
        }
        

       

        
    }


    // await using var command = dataSource.CreateCommand("INSERT INTO some_table (some_field) VALUES (8)");
    //await command.ExecuteNonQueryAsync();

    // ExecuteNonQueryASync -> does not return any results -> INSERT, UPDATE, DELETE
    // ExecuteScalarAsync -> returns a Value/Scalar
    // ExecuteReaderAsync -> return a result , use it for SELECT statement

    /* await using var cmd = new NpgsqlCommand("INSERT INTO table (col1) VALUES ($1), ($2)", conn)
    {
        Parameters =
        {
            new() { Value = "some_value" },
            new() { Value = "some_other_value" }
        }
    };

    await cmd.ExecuteNonQueryAsync();

    */

    /*
    
    Можно попробовать сделать через каждый раз когда мы делаем запрос
    using (SqlConnection conn = new SqlConnection()) 
    {

    }
    Но можно и упростить и просто открывать и закрывать? 
    Может сделать через конструктор

    */

