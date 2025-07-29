using BestBuyBestPractices;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

string connString = config.GetConnectionString("DefaultConnection");

IDbConnection conn = new MySqlConnection(connString);

#region Department Section
//var departmentRepo = new DapperDepartmentRepository(conn);

//departmentRepo.InsertDepartment("Toys");

//var departments = departmentRepo.GetAllDepartments();

//foreach (var department in departments)
//{
//    Console.WriteLine(department.DepartmentID);
//    Console.WriteLine(department.Name);
//    Console.WriteLine();
//    Console.WriteLine();
//}
#endregion

var productRepository = new DapperProductRepository(conn);

var productToUpdate = productRepository.GetProduct(942);
productRepository.UpdateProduct(productToUpdate);

productToUpdate.Name = "Updated!!";
productToUpdate.Price = 12.99;
productToUpdate.CategoryID = 1;
productToUpdate.OnSale = false;
productToUpdate.StockLevel = 1000;

productRepository.UpdateProduct(productToUpdate);

productRepository.DeleteProduct(943);

var products = productRepository.GetAllProducts();

foreach(var product in products)
{
    Console.WriteLine(product.ProductID);
    Console.WriteLine(product.Name);
    Console.WriteLine(product.Price);
    Console.WriteLine(product.CategoryID);
    Console.WriteLine(product.OnSale);
    Console.WriteLine(product.StockLevel);
    Console.WriteLine();
    Console.WriteLine();
}