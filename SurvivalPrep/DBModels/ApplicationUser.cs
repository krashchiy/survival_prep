using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurvivalPrep.DBModels
{
    public class ApplicationUser : IdentityUser, IComparable<ApplicationUser>
    {
        public int Money { get; set; }
        public ICollection<ItemInstance> OwnedItems { get; set; }
        [NotMapped]
        public int score => TotalScore();

        public int TotalScore()
        {
            int score = 0;
            foreach(ItemInstance item in OwnedItems)
            {
                score += item.Item.Score * item.Quantity;
            }
            return score;
        }

        public int DisasterScore(Disaster disaster)
        {
            int score = 0;
            foreach (ItemInstance item in OwnedItems)
            {
                if(item.Item.ItemDisasters.Intersect(disaster.ItemDisasters).Any())
                {
                    score += item.Item.Score * item.Quantity;
                }
                
            }
            return score;
        }

        public int CompareTo(ApplicationUser other)
        {
            return other.TotalScore().CompareTo(TotalScore());
        }
    }
}
