using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MediaMicroservice.ServiceCalls
{
    public interface IItemForSaleService
    {
        public Task<T> GetItemForSaleById<T>(HttpMethod method, Guid itemForSaleId);
    }
}
