using Microsoft.EntityFrameworkCore;
using SurvivalPrep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurvivalPrep.Areas.Identity.Data
{
    public class UserDBInitializer
    {
        public static void Initialize(UsersRolesDB db)
        {
            db.Database.Migrate();
        }
    }
}
