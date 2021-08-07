using AdMicroservice.DBContexts;
using AdMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data.PastPrices
{
    public class PastPriceRepository : IPastPriceRepository
    {

        private readonly ItemForSaleDbContext context;

        public PastPriceRepository(ItemForSaleDbContext context)
        {
            this.context = context;
        }

        public void CreatePastPrice(PastPrice pastPrice)
        {
            context.PastPrices.Add(pastPrice);
        }

        public void DeletePastPrice(int id)
        {
            var pastPrice = GetPastPriceById(id);
            context.Remove(pastPrice);
        }

        public PastPrice GetPastPriceById(int id)
        {
            return context.PastPrices.FirstOrDefault(e => e.PastPriceId == id);
        }

        public List<PastPrice> GetPastPriceByItemForSaleId(Guid id)
        {
            return context.PastPrices.Where(e => e.ItemForSaleId == id).ToList();
        }

        public List<PastPrice> GetPastPrices()
        {
            return context.PastPrices.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdatePastPrice(PastPrice oldPastPrice, PastPrice newPastPrice)
        {
            oldPastPrice.Price = newPastPrice.Price;
            oldPastPrice.ItemForSaleId = newPastPrice.ItemForSaleId;
        }
    }
}
