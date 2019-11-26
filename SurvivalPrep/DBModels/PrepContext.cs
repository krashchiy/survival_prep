using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SurvivalPrep.DBModels
{
    public class PrepContext: IdentityDbContext<ApplicationUser>
    {
        public PrepContext(DbContextOptions<PrepContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ItemDisaster>().HasKey(id => new { id.ItemId, id.DisasterId });
            modelBuilder.Entity<ItemInstance>().HasKey(item => new { item.ItemId, item.ApplicationUserID });
        }

        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Disaster> Disasters { get; set; }
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
