using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurvivalPrep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurvivalPrep.DBModels
{
    public class UserDBInitializer
    {
        public static void Initialize(PrepContext db, UserManager<ApplicationUser> userManager)
        {
            db.Database.Migrate();

            if (db.Users.Any())
            {
                // Data already seeded
                return;
            }

            // Seed items/disasters
            Disaster wildfire = new Disaster { Name = "Wildfire" };
            Disaster hurricane = new Disaster { Name = "Hurricane" };
            Disaster earthquake = new Disaster { Name = "Earthquake" };
            Disaster volcano = new Disaster { Name = "Volcanic Eruption" };
            Disaster flood = new Disaster { Name = "Flood" };

            Item respirator = new Item { Name = "Respirator", Cost = 100, Score = 150, ImageLink= "https://i.imgur.com/JBU4bWi.jpg" };
            respirator.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = wildfire,
                        Item = respirator
                    },
                    new ItemDisaster
                    {
                        Disaster = volcano,
                        Item = respirator
                    }

                };
            db.Items.Add(respirator);
            db.SaveChanges();

            Item inf_rowboat = new Item { Name = "Inflatable Rowboat", Cost = 150, Score = 200, ImageLink = "https://i.imgur.com/VQMKmes.jpg" };
            inf_rowboat.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = flood,
                        Item = inf_rowboat
                    }
                };
            db.Items.Add(inf_rowboat);
            db.SaveChanges();
            
            Item rowboat = new Item { Name = "Rowboat", Cost = 100, Score = 100, ImageLink = "https://images-na.ssl-images-amazon.com/images/I/41wIXf%2B43uL._SR600%2C315_PIWhiteStrip%2CBottomLeft%2C0%2C35_SCLZZZZZZZ_.jpg" };
            rowboat.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = flood,
                        Item = rowboat
                    }
                };
            db.Items.Add(rowboat);
            db.SaveChanges();
            
            Item compass = new Item { Name = "Compass", Cost = 20, Score = 30, ImageLink = "https://i.imgur.com/pAkP379.jpg" };
            compass.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = wildfire,
                        Item = compass
                    }
                };
            db.Items.Add(compass);
            db.SaveChanges();

            Item hardhat = new Item { Name = "Hard Hat", Cost = 20, Score = 50, ImageLink = "https://i.imgur.com/F48N5Zm.jpg" };
            hardhat.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = earthquake,
                        Item = hardhat
                    }
                };
            db.Items.Add(hardhat);
            db.SaveChanges();

            Item lifejacket = new Item { Name = "Life Jacket", Cost = 50, Score = 80, ImageLink = "https://i.imgur.com/UE8zNls.jpg" };
            lifejacket.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = flood,
                        Item = lifejacket
                    }
                };
            db.Items.Add(lifejacket);
            db.SaveChanges();

            Item rations = new Item { Name = "Rations", Cost = 30, Score = 10, ImageLink = "https://i.imgur.com/Nadoc5A.jpg" };
            rations.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = wildfire,
                        Item = rations
                    },
                    new ItemDisaster
                    {
                        Disaster = earthquake,
                        Item = rations
                    },
                    new ItemDisaster
                    {
                        Disaster = hurricane,
                        Item = rations
                    },
                    new ItemDisaster
                    {
                        Disaster = flood,
                        Item = rations
                    },
                    new ItemDisaster
                    {
                        Disaster = volcano,
                        Item = rations
                    }
                };
            db.Items.Add(rations);
            db.SaveChanges();

            Item generator = new Item { Name = "Generator", Cost = 500, Score = 250, ImageLink = "https://images-na.ssl-images-amazon.com/images/I/81TfpvWrBRL._SX425_.jpg" };
            generator.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = wildfire,
                        Item = generator
                    },
                    new ItemDisaster
                    {
                        Disaster = earthquake,
                        Item = generator
                    },
                    new ItemDisaster
                    {
                        Disaster = hurricane,
                        Item = generator
                    },
                    new ItemDisaster
                    {
                        Disaster = flood,
                        Item = generator
                    },
                    new ItemDisaster
                    {
                        Disaster = volcano,
                        Item = generator
                    }
                };
            db.Items.Add(generator);
            db.SaveChanges();
            
            Item sol_generator = new Item { Name = "Portable Solar Generator", Cost = 150, Score = 50, ImageLink = "https://images-na.ssl-images-amazon.com/images/I/71HLgifCw0L._SX425_.jpg" };
            sol_generator.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = wildfire,
                        Item = sol_generator
                    },
                    new ItemDisaster
                    {
                        Disaster = earthquake,
                        Item = sol_generator
                    },
                    new ItemDisaster
                    {
                        Disaster = hurricane,
                        Item = sol_generator
                    },
                    new ItemDisaster
                    {
                        Disaster = flood,
                        Item = sol_generator
                    },
                    new ItemDisaster
                    {
                        Disaster = volcano,
                        Item = sol_generator
                    }
                };
            db.Items.Add(sol_generator);
            db.SaveChanges();

            Item radios = new Item { Name = "Two-way radios waterproof", Cost = 100, Score = 50, ImageLink = "https://images-na.ssl-images-amazon.com/images/I/71Vph0r%2BPjL._AC_SL1200_.jpg" };
            radios.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = wildfire,
                        Item = radios
                    },
                    new ItemDisaster
                    {
                        Disaster = earthquake,
                        Item = radios
                    },
                    new ItemDisaster
                    {
                        Disaster = hurricane,
                        Item = radios
                    },
                    new ItemDisaster
                    {
                        Disaster = flood,
                        Item = radios
                    },
                    new ItemDisaster
                    {
                        Disaster = volcano,
                        Item = radios
                    }
                };
            db.Items.Add(radios);
            db.SaveChanges();

            Item sleep_bag = new Item { Name = "Sleeping bag", Cost = 100, Score = 100, ImageLink = "https://images-na.ssl-images-amazon.com/images/I/61ck7nqGhTL._AC_SL1000_.jpg" };
            sleep_bag.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = hurricane,
                        Item = sleep_bag
                    },
                    new ItemDisaster
                    {
                        Disaster = flood,
                        Item = sleep_bag
                    }
                };
            db.Items.Add(sleep_bag);
            db.SaveChanges();

            Item water = new Item { Name = "Water", Cost = 50, Score = 20, ImageLink = "https://i.imgur.com/K1KLw0I.jpg" };
            water.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = wildfire,
                        Item = water
                    },
                    new ItemDisaster
                    {
                        Disaster = earthquake,
                        Item = water
                    },
                    new ItemDisaster
                    {
                        Disaster = hurricane,
                        Item = water
                    },
                    new ItemDisaster
                    {
                        Disaster = flood,
                        Item = water
                    },
                    new ItemDisaster
                    {
                        Disaster = volcano,
                        Item = water
                    }
                };
            db.Items.Add(water);
            db.SaveChanges();
            
            Item emRations = new Item { Name = "Emergency Rations", Cost = 50, Score = 50, ImageLink = "https://images-na.ssl-images-amazon.com/images/I/91TkZbtY1qL._SX425_.jpg" };
            emRations.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = wildfire,
                        Item = emRations
                    },
                    new ItemDisaster
                    {
                        Disaster = earthquake,
                        Item = emRations
                    },
                    new ItemDisaster
                    {
                        Disaster = hurricane,
                        Item = emRations
                    },
                    new ItemDisaster
                    {
                        Disaster = flood,
                        Item = emRations
                    },
                    new ItemDisaster
                    {
                        Disaster = volcano,
                        Item = emRations
                    }
                };
            db.Items.Add(emRations);
            db.SaveChanges();

            Item firstaidkit = new Item { Name = "First-Aid Kit", Cost = 200, Score = 60, ImageLink = "https://i.imgur.com/wYHYCg9.jpg" };
            firstaidkit.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = wildfire,
                        Item = firstaidkit
                    },
                    new ItemDisaster
                    {
                        Disaster = earthquake,
                        Item = firstaidkit
                    },
                    new ItemDisaster
                    {
                        Disaster = hurricane,
                        Item = firstaidkit
                    },
                    new ItemDisaster
                    {
                        Disaster = flood,
                        Item = firstaidkit
                    }
                };
            db.Items.Add(firstaidkit);
            db.SaveChanges();

            Item extinguisher = new Item { Name = "Fire Extinguisher", Cost = 20, Score = 50, ImageLink = "https://i.imgur.com/lhnCE6R.jpg" };
            extinguisher.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = wildfire,
                        Item = extinguisher
                    },

                };
            db.Items.Add(extinguisher);
            db.SaveChanges();

            // Seed users
            ApplicationUser user1 = new ApplicationUser();
            user1.UserName = "user1@domain.com";
            user1.OwnedItems = new List<ItemInstance>
            {
                new ItemInstance{ApplicationUser=user1,ItemId=db.Items.Where(o => o.Name == respirator.Name).FirstOrDefault().ID,Quantity=2},
                new ItemInstance{ApplicationUser=user1,ItemId=db.Items.Where(o => o.Name == firstaidkit.Name).FirstOrDefault().ID,Quantity=1},
            };
            user1.Money = 2000;
            user1.EmailConfirmed = true;

            if (userManager.CreateAsync(user1, "123ABC!@#def").Result.Succeeded)
            {
                // Add roles if necessary
            }
            db.SaveChanges();

            ApplicationUser user2 = new ApplicationUser();
            user2.UserName = "user2@domain.com";
            user2.OwnedItems = new List<ItemInstance>
            {
                new ItemInstance{ApplicationUser=user2,ItemId=db.Items.Where(o => o.Name == compass.Name).FirstOrDefault().ID,Quantity=4},
                new ItemInstance{ApplicationUser=user2,ItemId=db.Items.Where(o => o.Name == firstaidkit.Name).FirstOrDefault().ID,Quantity=2},
            };
            user2.Money = 50;
            user2.EmailConfirmed = true;

            if (userManager.CreateAsync(user2, "123ABC!@#def").Result.Succeeded)
            {
                // Add roles if necessary
            }
            db.SaveChanges();

            ApplicationUser user3 = new ApplicationUser();
            user3.UserName = "user3@differentdomain.com";
            user3.OwnedItems = new List<ItemInstance>
            {
                new ItemInstance{ApplicationUser=user3,ItemId=db.Items.Where(o => o.Name == compass.Name).FirstOrDefault().ID,Quantity=1},
                new ItemInstance{ApplicationUser=user3,ItemId=db.Items.Where(o => o.Name == respirator.Name).FirstOrDefault().ID,Quantity=3},
            };
            user3.Money = 1000;
            user3.EmailConfirmed = true;

            if (userManager.CreateAsync(user3, "123ABC!@#def").Result.Succeeded)
            {
                // Add roles if necessary
            }
            db.SaveChanges();
        }
    }
}
