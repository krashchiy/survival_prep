using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
