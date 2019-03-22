using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebApp.Models
{
    [Owned]
    public class Bet
    {
        
        public int BetId { get; set; }
        public string Name { get; set; }
        public float BetPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Item Slot { get; set; }
        public virtual User Owner { get; set; }
    }
}
