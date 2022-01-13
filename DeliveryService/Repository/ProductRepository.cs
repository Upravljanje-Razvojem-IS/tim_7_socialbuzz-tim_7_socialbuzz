using DeliveryService.Database;
using DeliveryService.Interface;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductRepository product;
        private readonly DatabaseContext Context;
        public ProductRepository(DatabaseContext context)
        {
            Context = context;
        }


        public Product Add(Product product)
        {

            Context.Add(product);
            Context.SaveChanges();
            return product;
        }

        public void Delete(Guid id)
        {
            var product = Context.Products.Where(e => e.Id == id).FirstOrDefault();
            if (product == null)
                throw new ArgumentNullException("Product");
            else 
            {
                Context.Remove(product);
                Context.SaveChanges();
            }
        }

        public List<Product> GetAll()
        {
            var products = Context.Products.Select(c => new Product()
            {
                Id = c.Id,
                Name= c.Name,
                Quantity = c.Quantity,
                Price = c.Price,
                Description = c.Description
            }).ToList();

            return products;
        }

        public Product GetById(Guid id)
        {
            Product product = null;

            product = Context.Products.Where(e => e.Id == id).Select(c => new Product()
            {
                Id = c.Id,
                Name = c.Name,
                Quantity = c.Quantity,
                Price = c.Price,
                Description = c.Description
            }).FirstOrDefault();

            return product;
        }

        public Product Update(Guid id, Product product)
        {
            var updatedProduct = Context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (updatedProduct == null)
                throw new EntryPointNotFoundException();
            updatedProduct.Name = product.Name;
            updatedProduct.Quantity = product.Quantity;
            updatedProduct.Price = product.Price;
            updatedProduct.Description = product.Description;

            Context.SaveChanges();

            return updatedProduct;

        }
    }
}
