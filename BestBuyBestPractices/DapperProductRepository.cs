using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;
        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("select * from products;");
        }
        public Product GetProduct(int id)
        {
            return _conn.QuerySingle<Product>("select * from products where ProductID = @id;", new { id = id });
        }
        public void UpdateProduct(Product product)
        {
            _conn.Execute("update products" +
                " set name = @name," +
                " price = @price," +
                " categoryid = @catid," +
                " onsale = @onsale," +
                " stocklevel = @stock" +
                " where productid = @id;",
                new {
                    id = product.ProductID,
                    name = product.Name,
                    price = product.Price,
                    catid = product.CategoryID,
                    onsale = product.OnSale,
                    stock = product.StockLevel
                });
        }
        public void DeleteProduct(int id)
        {
            _conn.Execute("delete from products where productid = @id;", new {id = id});
            _conn.Execute("delete from reviews where productid = @id;", new { id = id });
            _conn.Execute("delete from sales where productid = @id;", new { id = id });
        }
    }
}
