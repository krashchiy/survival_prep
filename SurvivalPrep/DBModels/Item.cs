//Author: Nathan Robbins, Igor Ivanov, Jane Tian
//Date: Dec. 6, 2019
//Course: CS 4540, University of Utah, School of Computing
//Copyright: CS 4540 and Nathan Robbins, Igor Ivanov, Jane Tian - This work may not be copied for use in Academic Coursework.

//I, Nathan Robbins, certify that I wrote this code from scratch and did not copy it in part or whole from
//another source.Any references used in the completion of the assignment are cited in my README file.

//File Contents

//This file contains Item model and properties

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurvivalPrep.DBModels
{
    public class Item
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Cost { get; set; }
        public int Score { get; set; }
        public ICollection<ItemDisaster> ItemDisasters { get; set; }
        public ICollection<ItemInstance> ItemInstances { get; set; }
        public string ImageLink { get; set; }
    }
}
