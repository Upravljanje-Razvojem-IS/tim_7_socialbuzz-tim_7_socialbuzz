using PurchaseMicroservice.DBContexts;
using PurchaseMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Data.Purchases
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly PurchaseDbContext context;

        public PurchaseRepository(PurchaseDbContext context)
        {
            this.context = context;
        }
        public void CreatePurchase(Purchase purchase)
        {
            context.Purchases.Add(purchase);
        }


        public void DeletePurchase(Guid purchaseId)
        {
            var purchase = GetPurchaseById(purchaseId);
            if (purchase != null)
            {
                context.Remove(purchase);
            }
        }

        public List<Purchase> GetAllPurchase()
        {
            return context.Purchases.ToList();
        }


        public List<Purchase> GetPurchaseByAccountId(Guid id)
        {
            return context.Purchases.Where(e => (e.AccountId == id)).ToList();
        }

        public List<Purchase> GetPurchaseByDate(string date)
        {
            return context.Purchases.Where(e => (e.Date == date)).ToList();
        }

        public Purchase GetPurchaseById(Guid purchaseId)
        {
            return context.Purchases.FirstOrDefault(e => e.PurchaseId == purchaseId);
        }

        public List<Purchase> GetPurchaseByItemForSaleId(Guid id)
        {
            return context.Purchases.Where(e => (e.ItemForSaleId == id)).ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public void UpdatePurchase(Purchase oldPurchase, Purchase newPurchase)
        {
            oldPurchase.Description = newPurchase.Description;
        }
    }
}
