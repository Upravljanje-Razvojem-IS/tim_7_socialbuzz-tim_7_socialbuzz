using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using AccountService.Entities;
using AccountService.Data;

namespace AccountService.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AccountServiceContext context)
        {
            context.Database.EnsureCreated();

            //Look for any students.
            //if (context.AccUsers.Any())
            //{
            //    //return;   // DB has been seeded
            //}

            context.Database.EnsureDeleted();
            context.Database.Migrate();

            #region AccRole
            var accRoles = new AccRole[]
            {
                new AccRole {
                    Id = Guid.Parse("c8ae737c-ed9b-4fee-be6f-9c74ba376ae7"),
                    Name = "Administrator"
                },
                new AccRole {
                    Id = Guid.Parse("56860de3-f338-449c-be26-c946d4cb73b0"),
                    Name = "User"
                },
                new AccRole {
                    Id = Guid.Parse("5a1808ce-43b3-4e8b-9789-271b33b04f43"),
                    Name = "Corporation"
                }
            };
            foreach (AccRole ar in accRoles)
            {
                context.AccRoles.Add(ar);
            }
            context.SaveChanges();
            #endregion

            #region City
            var cities = new City[]
            {
                new City {
                    ID = Guid.Parse("e5a60ae7-4c82-4678-8d98-6c711d9c7d3f"),
                    Name = "Novi Sad"
                },
                new City {
                    ID = Guid.Parse("d09118cb-05b7-4d5a-864a-6fd5199b794a"),
                    Name = "Beograd"
                },
                new City {
                    ID = Guid.Parse("397f7650-6ef5-4b52-ad73-e6d3df1f2816"),
                    Name = "Nis"
                }
            };
            foreach (City c in cities)
            {
                context.Cities.Add(c);
            }
            context.SaveChanges();
            #endregion

            #region Accounts
            var accounts = new Account[]
            {
                new Account {
                    Id = Guid.Parse("259064f5-fdd5-4dbd-965e-79129c3bc0e3"),
                    FirstName = "Petar",
                    LastName = "Petrovic",
                    CityId = Guid.Parse("e5a60ae7-4c82-4678-8d98-6c711d9c7d3f"),
                    RoleId = Guid.Parse("c8ae737c-ed9b-4fee-be6f-9c74ba376ae7") // Administrator
                },
                new Account {
                    Id = Guid.Parse("d59101e6-feaa-421f-9392-793f86bdaf4b"),
                    FirstName = "Marko",
                    LastName = "Markovic",
                    CityId = Guid.Parse("e5a60ae7-4c82-4678-8d98-6c711d9c7d3f"),
                    RoleId = Guid.Parse("56860de3-f338-449c-be26-c946d4cb73b0") // User
                },
                new Account {
                    Id = Guid.Parse("1f11e055-0409-4467-b8d9-245049d69d7b"),
                    FirstName = "Nikola",
                    LastName = "Nikolic",
                    CityId = Guid.Parse("d09118cb-05b7-4d5a-864a-6fd5199b794a"),
                    RoleId = Guid.Parse("56860de3-f338-449c-be26-c946d4cb73b0") // User
                }
            };
            foreach (Account a in accounts)
            {
                context.Accounts.Add(a);
            }
            context.SaveChanges();
            #endregion
        }
    }
}