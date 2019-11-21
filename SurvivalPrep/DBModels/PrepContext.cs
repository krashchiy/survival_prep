using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SurvivalPrep.DBModels
{
    public class PrepContext: DbContext
    {
        public PrepContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Item> Items { get; set; }
    }

    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public string Answer { get; set; }

        public int QuestionCategoryId { get; set; }
        public QuestionCategory QuestionCategory { get; set; }
    }

    public class QuestionCategory
    {
        public int QuestionCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ScoreWeight { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
