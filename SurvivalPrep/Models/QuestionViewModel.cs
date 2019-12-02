using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurvivalPrep.DBModels;

namespace SurvivalPrep.Models
{
    public class QuestionViewModel
    {
        public List<QuestionCategory> QuestionCategories { get; set; }
        public Question Question { get; set; }
    }
}
