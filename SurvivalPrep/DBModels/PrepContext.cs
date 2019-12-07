//Author: Nathan Robbins, Igor Ivanov, Jane Tian
//Date: Dec. 6, 2019
//Course: CS 4540, University of Utah, School of Computing
//Copyright: CS 4540 and Nathan Robbins, Igor Ivanov, Jane Tian - This work may not be copied for use in Academic Coursework.

//We certify that I wrote this code from scratch and did not copy it in part or whole from
//another source.Any references used in the completion of the assignment are cited in my README file.

//File Contents:
//This file contains the definition of PrepContext class containing all models as DB sets

using System.Collections.Generic;
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
        public DbSet<ItemDisaster> ItemDisasters { get; set; }
        public DbSet<ItemInstance> ItemInstances { get; set; }
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

    public enum Score
    {
        All = 0,
        Beginner = 1,
        Medium = 2,
        Advanced = 3,
        Pro = 5
    }
}
