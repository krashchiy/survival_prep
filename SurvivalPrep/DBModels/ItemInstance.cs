using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurvivalPrep.DBModels
{
    public class ItemInstance
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int Quantity { get; set; }
    }
}
