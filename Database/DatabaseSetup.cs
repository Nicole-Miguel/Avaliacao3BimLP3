using Microsoft.Data.Sqlite;

namespace LabManager.Database;

class DatabaseSetup
{
    private DatabaseConfig databaseConfig;

    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
        CreateTableProduct();
    }

    private void CreateTableProduct()
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Products(
                id int not null primary key,
                name varchar(150) not null,
                price float not null,
                active boolean not null
            );
        ";
        command.ExecuteNonQuery();
        connection.Close();
    }
}