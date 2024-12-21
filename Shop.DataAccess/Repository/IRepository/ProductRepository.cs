using CakeShop.DataAccess.Data;
using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace CakeShop.DataAccess.Repository.IRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u=> u.Id ==obj.Id);
            if (objFromDb != null) { 
             
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.ProductId = obj.ProductId;    
                objFromDb.supplier = obj.supplier;
                objFromDb.ListPrice = obj.ListPrice;
                if (obj.ImageUrl != null) { 
                 objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
