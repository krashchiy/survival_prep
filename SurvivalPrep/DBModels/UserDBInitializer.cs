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

            Item rowboat = new Item { Name = "Inflatable Rowboat", Cost = 150, Score = 200, ImageLink = "https://i.imgur.com/b8KNPiU.jpg" };
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

            Item rations = new Item { Name = "Rations", Cost = 50, Score = 20, ImageLink = "https://i.imgur.com/Nadoc5A.jpg" };
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
