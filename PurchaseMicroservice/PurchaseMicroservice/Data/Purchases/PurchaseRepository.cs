using PurchaseMicroservice.DBContexts;
using PurchaseMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Data.Purchases
{
    /// <summary>
    /// 
    /// </summary>
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly PurchaseDbContext context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public PurchaseRepository(PurchaseDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchase"></param>
        public void CreatePurchase(Purchase purchase)
        {
            context.Purchases.Add(purchase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseId"></param>
        public void DeletePurchase(Guid purchaseId)
        {
            var purchase = GetPurchaseById(purchaseId);
            if (purchase != null)
            {
                context.Remove(purchase);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Purchase> GetAllPurchase()
        {
            return context.Purchases.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Purchase> GetPurchaseByAccountId(Guid id)
        {
            return context.Purchases.Where(e => (e.AccountId == id)).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Purchase> GetPurchaseByDate(string date)
        {
            return context.Purchases.Where(e => (e.Date == date)).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <returns></returns>
        public Purchase GetPurchaseById(Guid purchaseId)
        {
            return context.Purchases.FirstOrDefault(e => e.PurchaseId == purchaseId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Purchase> GetPurchaseByItemForSaleId(Guid id)
        {
            return context.Purchases.Where(e => (e.ItemForSaleId == id)).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldPurchase"></param>
        /// <param name="newPurchase"></param>
        public void UpdatePurchase(Purchase oldPurchase, Purchase newPurchase)
        {
            oldPurchase.Description = newPurchase.Description;
        }
    }
}
