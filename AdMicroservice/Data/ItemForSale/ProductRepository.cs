using AdMicroservice.Data.PastPrices;
using AdMicroservice.DBContexts;
using AdMicroservice.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data.ItemForSale
{
    public class ProductRepository : IProductRepository
    {
        private readonly ItemForSaleDbContext context;
        private readonly IPastPriceRepository pastPriceRepository;


        public ProductRepository(ItemForSaleDbContext context, IPastPriceRepository pastPriceRepository)
        {
            this.context = context;
            this.pastPriceRepository = pastPriceRepository;
        }

        public void CreateProduct(Product product)
        {
            context.Products.Add(product);
        }

        public void DeleteProduct(Guid id)
        {
            var product = GetProductById(id);
            if (product!=null)
            {
                context.Remove(product);
            }
 
        }

        public Product GetProductById(Guid id)
        {
            return context.Products.FirstOrDefault(e => e.ItemForSaleId == id);
        }

        public List<Product> GetProducts(string pName = null)
        {
            return context.Products.Where(e => (pName == null || e.Name == pName)).ToList();
        }

        public List<Product> GetProductsByAccountId(Guid id)
        {
            return context.Products.Where(e => (e.AccountId == id)).ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        //sacuvam staru cenu nakon promene
        public void UpdateProduct(Product oldProduct, Product newProduct)
        {
            oldProduct.Name = newProduct.Name;
            oldProduct.Description = newProduct.Description;
            oldProduct.AccountId = newProduct.AccountId;
            oldProduct.Weight = newProduct.Weight;
            if(oldProduct.Price!=newProduct.Price)
            {
                PastPrice pastPrice = new PastPrice();
                pastPrice.Price = oldProduct.Price;
                pastPrice.ItemForSaleId = oldProduct.ItemForSaleId;
                oldProduct.Price = newProduct.Price;
                pastPriceRepository.CreatePastPrice(pastPrice);
            }
        }
    }
}
