using Microsoft.AspNetCore.Mvc;
using Npgsql;
namespace redlist_birds_api.DatabaseConnection;

public class DatabaseSetup
{
    public string connectionString = "Host = localhost; Username = postgres; Password = Emulka7823; Database = dbforbirds";
    // Connection string to Database

    // Если мы не используем Data Source -> то нужно самим открывать соединения коммандами (openAsync)
    // Если мы используем Data Source -> то Npgsql все сделает за нас..
    public async Task OpenConnection(string conString)
    {
        await using var conn = new NpgsqlConnection(conString);
        await conn.OpenAsync();
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

}