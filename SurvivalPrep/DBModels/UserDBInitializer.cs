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

            // Seed disasters
            var disasters = new Disaster[]
            {
                new Disaster{Name="Typhoon"},
            };
            foreach ( Disaster d in disasters)
            {
                db.Disasters.Add(d);
            }
            db.SaveChanges();

            // Seed items
            var wildfire = new Disaster { Name = "Wildfire" };

            var respirator = new Item { Name = "Respirator", Cost = 100, Score = 150, ImageLink= "https://i.imgur.com/JBU4bWi.jpg" };
            respirator.ItemDisasters = new List<ItemDisaster>
                {
                    new ItemDisaster
                    {
                        Disaster = wildfire,
                        Item = respirator
                    }

                };
            db.Items.Add(respirator);
            db.SaveChanges();

            // Seed users
            ApplicationUser user1 = new ApplicationUser();
            user1.UserName = "user1@domain.com";
            Item item = db.Items.Where(o => o.Name == "Respirator").FirstOrDefault();
            user1.OwnedItems = new List<ItemInstance>
            {
                new ItemInstance{ApplicationUser=user1,ItemId=item.ID,Quantity=2}
            };
            user1.Money = 2000;
            user1.EmailConfirmed = true;

            if (userManager.CreateAsync(user1, "123ABC!@#def").Result.Succeeded)
            {
                // Add roles if necessary
            }
            db.SaveChanges();
        }
    }
}
