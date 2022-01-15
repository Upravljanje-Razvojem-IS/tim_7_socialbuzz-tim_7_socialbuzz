using DeliveryService.DTOs.ProductDTOs;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Interface
{
    public interface IProductRepository
    {
        List<ProductDTO> GetAll();
        ProductDTO GetById(Guid id);
        ProductDTO Add(ProductCreateDTO product);
        ProductConfirmDTO Update(Guid id, ProductDTO product);
        void Delete(Guid id);
    }
}
