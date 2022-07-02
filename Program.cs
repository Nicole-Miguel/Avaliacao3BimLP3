using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var databaseConfig = new DatabaseConfig();
new DatabaseSetup(databaseConfig);

//Routing
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Product")
{
    var productRepository = new ProductRepository(databaseConfig);

    if(modelAction == "List")
    {
        Console.WriteLine("Product List");
        if(productRepository.GetAll().Any()) {
            foreach (var product in productRepository.GetAll())
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, Math.Round(product.Price, 2), product.Active);
            } 
        } else {
            Console.WriteLine("Nenhum produto cadastrado.");
        }
        
    }

    if(modelAction == "New")
    {
        Console.WriteLine("Product New");
        int id = Convert.ToInt32(args[2]);
        var name = args[3];
        double price = Convert.ToDouble(args[4]);
        bool active = Convert.ToBoolean(args[5]);

        if(productRepository.ExistsById(id)) {
            Console.WriteLine($"Produto com Id {id} já existe.");
        } else {
            var product = new Product (id, name, price, active);
            productRepository.Save(product); 
            Console.WriteLine($"Produto {name} cadastrado com sucesso.");
        }   
    }
    
    if(modelAction == "Delete")
    {
        Console.WriteLine("Product Delete");
        var id = Convert.ToInt32(args[2]);

        if(productRepository.ExistsById(id))
        {
            productRepository.Delete(id);
            Console.WriteLine($"Produto {id} removido com sucesso.");
        } else {
             Console.WriteLine($"Produto {id} não encontrado.");
        }  
    }

    if(modelAction == "Enable")
    {
        Console.WriteLine("Product Enable");
        var id = Convert.ToInt32(args[2]);
      
        if(productRepository.ExistsById(id))
        {
            productRepository.Enable(id);
            Console.WriteLine($"Produto {id} habilitado com sucesso."); 
        } else {
            Console.WriteLine($"Produto {id} não encontrado.");
        }   
    }

    if(modelAction == "Disable")
    {
        Console.WriteLine("Product Disable");
        var id = Convert.ToInt32(args[2]);
      
        if(productRepository.ExistsById(id))
        {
            productRepository.Disable(id);
            Console.WriteLine($"Produto {id} desabilitado com sucesso."); 
        } else {
            Console.WriteLine($"Produto {id} não encontrado.");
        }   
    }

    if(modelAction == "PriceBetween")
    {
        Console.WriteLine("Product PriceBetween");
        var initialPrice = Convert.ToDouble(args[2]);
        var endPrice = Convert.ToDouble(args[3]);

        if(productRepository.GetAllWithPriceBetween(initialPrice, endPrice).Any()) {
            foreach (var product in productRepository.GetAllWithPriceBetween(initialPrice, endPrice))
            {
                Console.WriteLine ("{0}, {1}, {2}, {3}", product.Id, product.Name, Math.Round(product.Price, 2), product.Active);
            }
        } else {
            Console.WriteLine ($"Nenhum produto encontrado dentro do intervalo de preço {Math.Round(initialPrice, 2)} e R$ {Math.Round(endPrice, 2)}.");
        }    
    }

    if(modelAction == "PriceHigherThan")
    {
        Console.WriteLine("Product PriceHigherThan");
        var price = Convert.ToDouble(args[2]);
        
        if(productRepository.GetAllWithPriceHigherThan(price).Any()) {
            foreach (var product in productRepository.GetAllWithPriceHigherThan(price))
            {
                Console.WriteLine ("{0}, {1}, {2}, {3}", product.Id, product.Name, Math.Round(product.Price, 2), product.Active);
            }
        } else {
            Console.WriteLine ($"Nenhum produto encontrado com preço maior que R$ {Math.Round(price, 2)}.");
        }  
    }

    if(modelAction == "PriceLowerThan")
    {
        Console.WriteLine("Product PriceLowerThan");
        var price = float.Parse(args[2]);
        
        if(productRepository.GetAllWithPriceLowerThan(price).Any()) {
            foreach (var product in productRepository.GetAllWithPriceLowerThan(price))
            {
                Console.WriteLine ("{0}, {1}, {2}, {3}", product.Id, product.Name, Math.Round(product.Price, 2), product.Active);
            }
        } else {
            Console.WriteLine ($"Nenhum produto encontrado com preço menor que R$ {Math.Round(price, 2)}.");
        }  
    }

    if(modelAction == "AveragePrice") 
    {
        Console.WriteLine("Product AveragePrice");
        if(productRepository.GetAll().Any()) {
            Console.WriteLine("A média dos preços é R$ {0}.", Math.Round(productRepository.GetAveragePrice(),2));
        } else {
            Console.WriteLine("Nenhum produto cadastrado.");
        }
    }
}
