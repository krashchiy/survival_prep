//Author: Nathan Robbins, Igor Ivanov, Jane Tian
//Date: Dec. 6, 2019
//Course: CS 4540, University of Utah, School of Computing
//Copyright: CS 4540 and Nathan Robbins, Igor Ivanov, Jane Tian - This work may not be copied for use in Academic Coursework.

//I, Igor Ivanov that I wrote this code from scratch and did not copy it in part or whole from
//another source.Any references used in the completion of the assignment are cited in my README file.

//File Contents:
//Custom view model for challenges view

using System.Collections.Generic;
using SurvivalPrep.DBModels;

namespace SurvivalPrep.Models
{
    public class QuestionViewModel
    {
        public List<QuestionCategory> QuestionCategories { get; set; }
        public Question Question { get; set; }
    }
}
