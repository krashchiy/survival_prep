//Author: Nathan Robbins, Igor Ivanov, Jane Tian
//Date: Dec. 6, 2019
//Course: CS 4540, University of Utah, School of Computing
//Copyright: CS 4540 and Nathan Robbins, Igor Ivanov, Jane Tian - This work may not be copied for use in Academic Coursework.

//I, Nathan Robbins, certify that I wrote this code from scratch and did not copy it in part or whole from
//another source.Any references used in the completion of the assignment are cited in my README file.

//File Contents:
//This file contains ApplicationUser model and properties. It is an extension to IdentityUser

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
