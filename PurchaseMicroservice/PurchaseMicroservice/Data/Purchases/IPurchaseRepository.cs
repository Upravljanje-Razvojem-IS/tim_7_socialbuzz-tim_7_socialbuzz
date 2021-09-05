using PurchaseMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Data.Purchases
{
    public interface IPurchaseRepository
    {
        List<Purchase> GetAllPurchase();
        Purchase GetPurchaseById(Guid purchaseId);
        void CreatePurchase(Purchase purchase);
        void UpdatePurchase(Purchase oldPurchase, Purchase newPurchase);
        void DeletePurchase(Guid purchaseId);
        bool SaveChanges();
        List<Purchase> GetPurchaseByDate(String date);
        List<Purchase> GetPurchaseByAccountId(Guid id);
        List<Purchase> GetPurchaseByItemForSaleId(Guid id);
    }
}
