using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SurvivalPrep.DBModels;

namespace SurvivalPrep.DBModels
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PrepContext(serviceProvider.GetRequiredService<DbContextOptions<PrepContext>>()))
            {
                context.Database.Migrate();

                if (context.Items.Any())
                {
                    return;   // DB has been seeded
                }

                context.Items.AddRange
                (
                new Item{Name="Fire extinguisher",Score=10,Cost=50},
                new Item{Name="Life jacket",Score=10,Cost=60},
                new Item{Name="Rowboat",Score=20,Cost=100},
                new Item{Name="Water",Score=10,Cost=50},
                new Item{Name="Rations",Score=10,Cost=50},
                new Item{Name="Hard hat",Score=5,Cost=20},
                new Item{Name="Something",Score=10,Cost=100},
                new Item{Name="Something",Score=10,Cost=100},
                new Item{Name="Something",Score=10,Cost=100},
                new Item{Name="Something",Score=10,Cost=100}
                );
                context.SaveChanges();
            }
            

        }
    }
}
