using LabManager.Models;
using LabManager.Database;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class ProductRepository
{
    private DatabaseConfig databaseConfig;

    public ProductRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public Product Save(Product product) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Products VALUES(@Id, @Name, @Price, @Active)", product);
     
        return product;
    }

     public void Delete(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Products WHERE id = @Id", new {Id = id});
    }

    public void Enable(int id) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET active = @Active WHERE id = @Id", new {Active = true, Id = id});
    }

    public void Disable(int id) {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET active = @Active WHERE id = @Id", new {Active = false, Id = id});
    } 

    public List<Product> GetAll()
    {
       using var connection = new SqliteConnection(databaseConfig.ConnectionString);
       connection.Open();

       var products = connection.Query<Product>("SELECT * FROM Products").ToList();

       return products;
    }

    public List<Product> GetAllWithPriceBetween(double initialPrice, double endPrice) {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
       connection.Open();

       var products = connection.Query<Product>("SELECT * FROM Products WHERE price >= @initialPrice  AND price <= @endPrice", new{initialPrice = initialPrice, endPrice = endPrice}).ToList();

       return products;
    }

    public List<Product> GetAllWithPriceHigherThan(double price) {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
       connection.Open();

       var products = connection.Query<Product>("SELECT * FROM Products WHERE price > @Price", new {Price = price}).ToList();

       return products;
    }

    public List<Product> GetAllWithPriceLowerThan(double price) {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
       connection.Open();

       var products = connection.Query<Product>("SELECT * FROM Products WHERE price < @Price", new {Price = price}).ToList();

       return products;
    }

    public double GetAveragePrice() {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
       connection.Open();

       var average = connection.ExecuteScalar<double>("SELECT AVG (price) FROM Products");
       
       return average;
    }

    public Product GetById(int id) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var product = connection.QuerySingle<Product>("SELECT * FROM Products WHERE id = @Id", new {Id = id});

        return product;
    }

    public bool ExistsById(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.ExecuteScalar<bool> ("SELECT count(id) FROM Products WHERE id= @Id", new {Id = id});
        
       return result;
    }
}