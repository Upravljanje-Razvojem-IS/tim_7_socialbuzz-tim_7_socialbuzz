using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Interface
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(Guid id);
        Product Add(Product product);
        Product Update(Guid id, Product product);
        void Delete(Guid id);
    }
}
