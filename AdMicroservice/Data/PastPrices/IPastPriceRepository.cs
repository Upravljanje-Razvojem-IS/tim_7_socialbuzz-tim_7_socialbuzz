using AdMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data.PastPrices
{
    public interface IPastPriceRepository
    {
        List<PastPrice> GetPastPrices();

        PastPrice GetPastPriceById(int id);

        void CreatePastPrice(PastPrice pastPrice);

        void UpdatePastPrice(PastPrice oldPastPrice, PastPrice newPastPrice);

        void DeletePastPrice(int id);

        bool SaveChanges();

        List<PastPrice> GetPastPriceByItemForSaleId(Guid id);
    }
}
