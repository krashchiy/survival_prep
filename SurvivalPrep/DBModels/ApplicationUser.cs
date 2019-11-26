using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurvivalPrep.DBModels
{
    public class ApplicationUser : IdentityUser
    {
        public int Money { get; set; }
        public ICollection<ItemInstance> OwnedItems { get; set; }
    }
}
