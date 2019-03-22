using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuctionWebApp.Models
{
    [Owned]
    public class Item 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public float StartPrice { get; set; }
        public float BuyoutPrice { get; set; }
        public float Term { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime RemoveDate { get; set; }
        public bool PurchasedStatus { get; set; }

        public virtual User Owner { get; set; }
        public virtual List<Bet> Bets { get; set; }
    }
}
