using PurchaseMicroservice.Models.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Data.DeliveryMock
{
    /// <summary>
    /// 
    /// </summary>
    public class DeliveryMockRepository : IDeliveryMockRepository

        
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<DeliveryDTO> Deliveries { get; set; } = new List<DeliveryDTO>();
        /// <summary>
        /// 
        /// </summary>
        public DeliveryMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            Deliveries.AddRange(new List<DeliveryDTO>
            {
                new DeliveryDTO
                {
                    DeliveryId = Guid.Parse("e04ce688-4fe1-43a1-838e-e3aedbe083b8"),
                    Address = "220 Washington, Syracuse",
                    Price = "3$"
                },
                new DeliveryDTO
                {
                    DeliveryId = Guid.Parse("bea55668-1ab8-4aa7-aca3-9b184f2c80d1"),
                    Address = "123 N Main St, Fairmount",
                    Price = "2.5$"
                },
                new DeliveryDTO
                {
                    DeliveryId = Guid.Parse("f3c1d7f0-f973-4e11-9d99-35bf23f88304"),
                    Address = "30 Commercial Road, Fratton",
                    Price = "4$"
                }
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DeliveryDTO GetDeliveryByID(Guid id)
        {
            return Deliveries.FirstOrDefault(e => e.DeliveryId == id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public DeliveryDTO GetDeliveryByAddress(string address)
        {
            return Deliveries.FirstOrDefault(e => e.Address == address);
        }
    }
}
