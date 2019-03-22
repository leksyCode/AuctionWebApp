using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebApp.Models
{
    [Owned]
    public class User : IdentityUser
    {
        public int Year { get; set; }
        public long Balance { get; set; }

        // TODO: More I can try to uncoment Items
        //public virtual List<Item> Items { get; set; }
        //public virtual List<Bet> Bets { get; set; }
    }
}
 