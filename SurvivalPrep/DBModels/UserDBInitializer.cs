using Microsoft.EntityFrameworkCore;
using SurvivalPrep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurvivalPrep.DBModels
{
    public class UserDBInitializer
    {
        public static void Initialize(PrepContext db)
        {
            db.Database.Migrate();
        }
    }
}
