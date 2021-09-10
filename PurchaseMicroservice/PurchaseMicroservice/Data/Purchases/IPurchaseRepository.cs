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
    public interface IPurchaseRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Purchase> GetAllPurchase();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <returns></returns>
        Purchase GetPurchaseById(Guid purchaseId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchase"></param>
        void CreatePurchase(Purchase purchase);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldPurchase"></param>
        /// <param name="newPurchase"></param>
        void UpdatePurchase(Purchase oldPurchase, Purchase newPurchase);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseId"></param>
        void DeletePurchase(Guid purchaseId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();
        /// <summary>
        /// get all purchases by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        List<Purchase> GetPurchaseByDate(String date);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Purchase> GetPurchaseByAccountId(Guid id);
        /// <summary>
        /// get purchase by item for sale id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Purchase> GetPurchaseByItemForSaleId(Guid id);
    }
}
