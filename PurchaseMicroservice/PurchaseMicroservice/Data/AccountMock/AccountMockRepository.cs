using PurchaseMicroservice.Models.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Data.AccountMock
{
    public class AccountMockRepository : IAccountMockRepository
    {
        public static List<AccountDTO> Accounts { get; set; } = new List<AccountDTO>();

        public AccountMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            Accounts.AddRange(new List<AccountDTO>
            {
                new AccountDTO
                {
                    AccountId = Guid.Parse("7e58590d-3968-4730-9c62-1ce5ae071478"),
                    FirstName = "Sanja",
                    LastName = "Knezevic"
                },
                new AccountDTO
                {
                    AccountId = Guid.Parse("1265bcd7-6e5a-491b-b802-acf4c4646d78"),
                    FirstName = "Nikola",
                    LastName = "Joksovic"
                },
                new AccountDTO
                {
                    AccountId = Guid.Parse("bcac549f-188c-415b-b64f-6ac5545a3c17"),
                    FirstName = "Katarina",
                    LastName = "Misic"
                }
            });
        }
        public AccountDTO GetAccountByFirstName(string firstName)
        {
            return Accounts.FirstOrDefault(e => e.FirstName == firstName);
        }

        public AccountDTO GetAccountByID(Guid id)
        {
            return Accounts.FirstOrDefault(e => e.AccountId == id);
        }

        public AccountDTO GetAccountByLasttName(string lastName)
        {
            return Accounts.FirstOrDefault(e => e.LastName == lastName);
        }
    }
}
