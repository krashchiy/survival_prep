//Author: Nathan Robbins, Igor Ivanov, Jane Tian
//Date: Dec. 6, 2019
//Course: CS 4540, University of Utah, School of Computing
//Copyright: CS 4540 and Nathan Robbins, Igor Ivanov, Jane Tian - This work may not be copied for use in Academic Coursework.

//I, Nathan Robbins, certify that I wrote this code from scratch and did not copy it in part or whole from
//another source.Any references used in the completion of the assignment are cited in my README file.

//File Contents:
//This file contains ItemDisaster model and properties

namespace SurvivalPrep.DBModels
{
    public class ItemDisaster
    {
        public int ItemId { get; set; }
        public Item Item {get; set;}
        public int DisasterId { get; set; }
        public Disaster Disaster { get; set; }
    }
}
