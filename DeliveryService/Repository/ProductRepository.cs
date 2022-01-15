using AutoMapper;
using DeliveryService.Database;
using DeliveryService.DTOs.ProductDTOs;
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
        private readonly DatabaseContext Context;
        private readonly IMapper Mapper;
        public ProductRepository(DatabaseContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }


        public ProductDTO Add(ProductCreateDTO product)
        {
            Product addedProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Quantity = product.Quantity,
                Price = product.Price,
                Description = product.Description
            };

            Context.Products.Add(addedProduct);
            Context.SaveChanges();

            return Mapper.Map<ProductDTO>(addedProduct);
        }

        public void Delete(Guid id)
        {
            var product = Context.Products.Where(e => e.Id == id).FirstOrDefault();
            if (product == null)
                throw new ArgumentNullException("Product does not exist");
            else 
            {
                Context.Remove(product);
                Context.SaveChanges();
            }
        }

        public List<ProductDTO> GetAll()
        {
            var products = Context.Products.Select(c => new Product()
            {
                Id = c.Id,
                Name= c.Name,
                Quantity = c.Quantity,
                Price = c.Price,
                Description = c.Description
            }).ToList();

            return Mapper.Map<List<ProductDTO>>(products);
        }

        public ProductDTO GetById(Guid id)
        {

            var product = Context.Products.Where(e => e.Id == id).Select(c => new Product()
            {
                Id = c.Id,
                Name = c.Name,
                Quantity = c.Quantity,
                Price = c.Price,
                Description = c.Description
            }).FirstOrDefault();

            return Mapper.Map<ProductDTO>(product);
        }

        public ProductConfirmDTO Update(Guid id, ProductDTO product)
        {
            var updatedProduct = Context.Products.FirstOrDefault(x => x.Id == id);
            if (updatedProduct == null)
                throw new EntryPointNotFoundException();
            updatedProduct.Name = product.Name;
            updatedProduct.Quantity = product.Quantity;
            updatedProduct.Price = product.Price;
            updatedProduct.Description = product.Description;

            Context.SaveChanges();

            return Mapper.Map<ProductConfirmDTO>(updatedProduct);

        }
    }
}
