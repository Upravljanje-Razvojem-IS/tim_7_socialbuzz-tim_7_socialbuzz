using PurchaseMicroservice.Models.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Data.ItemForSaleMock
{
    /// <summary>
    /// 
    /// </summary>
    public class ItemForSaleMockRepository : IItemForSaleMockRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<ItemForSaleDTO> Items { get; set; } = new List<ItemForSaleDTO>();
        /// <summary>
        /// 
        /// </summary>

        public ItemForSaleMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            Items.AddRange(new List<ItemForSaleDTO>
            {
                new ItemForSaleDTO
                {
                    ItemForSaleId = Guid.Parse("915510b2-74fb-44b7-b265-730ac0079a0d"),
                    Name = "Xiaomi Redmi Note 9 Pro",
                    Price = "300$"
                },
                new ItemForSaleDTO
                {
                    ItemForSaleId = Guid.Parse("972379ca-7d10-4741-9b61-90414c77f32d"),
                    Name = "Pencil case",
                    Price = "25$"
                },
                new ItemForSaleDTO
                {
                    ItemForSaleId = Guid.Parse("fe7a9096-80f4-4b7f-a8c3-0029be9959b7"),
                    Name = "Zara skirt M",
                    Price = "50$"
                }
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ItemForSaleDTO GetItemForSaleByID(Guid id)
        {
            return Items.FirstOrDefault(e => e.ItemForSaleId == id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ItemForSaleDTO GetItemForSaleByName(string name)
        {
            return Items.FirstOrDefault(e => e.Name == name);
        }
    }
}
