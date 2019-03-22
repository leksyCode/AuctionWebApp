using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuctionWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AuctionWebApp.Controllers
{
    public class BetsController : Controller
    {
        public static User user = null;

        private AuctionIdentityContext db;
        private UserManager<User> _userManager;

        public BetsController(AuctionIdentityContext context, UserManager<User> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(db.Bets.Include(u => u.Owner).Include(s => s.Slot).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Bet bet, int? id)
        {
            Item item = await db.Items.Include(p => p.Owner).FirstOrDefaultAsync(p => p.Id == id);
            user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (bet.BetPrice >= item.BuyoutPrice && item.PurchasedStatus != true)
            {
                item.PurchasedStatus = true;
                item.Owner = user;
            }           
            bet.Owner = user;
            bet.Slot = item;
            bet.CreateDate = DateTime.Now;
            db.Bets.Add(bet);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Bets");          
        }

        public IActionResult Buyout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buyout(Bet bet, int? id)
        {
            Item item = await db.Items.Include(p => p.Owner).FirstOrDefaultAsync(p => p.Id == id);
            user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (item.PurchasedStatus != true)
            {
                item.PurchasedStatus = true;
                item.Owner = user;
            }
            bet.BetPrice = item.BuyoutPrice;
            bet.Owner = user;
            bet.Slot = item;
            bet.CreateDate = DateTime.Now;
            db.Bets.Add(bet);
            await db.SaveChangesAsync();

            return View();
        }
    }
}
