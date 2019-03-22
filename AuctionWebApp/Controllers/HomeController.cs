using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.IO;
using AuctionWebApp.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace EFDataApp.Controllers
{
   
    [Authorize]
    public class HomeController : Controller
    {
        public static User user = null;

        private AuctionIdentityContext db;
        private UserManager<User> _userManager;

        public HomeController(AuctionIdentityContext context, UserManager<User> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            foreach (var item in db.Items.Include(o => o.Owner).Include(b => b.Bets))
            {
                if (item.RemoveDate <= DateTime.Now)
                {
                    db.Items.Remove(item);
                }
            }
          
            await db.SaveChangesAsync();
            
            return View(db.Items.Include(p => p.Owner).Include(b => b.Bets).Where(s => s.PurchasedStatus != true).ToList());
        }

        public IActionResult Create()
        {
            return View();  
        }

        // CREATE


        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            string html = GetImageFromGoogle.GetHtmlCode(item.Name);
            item.ImgUrl = GetImageFromGoogle.GetUrls(html);
            item.CreateDate = DateTime.Now;
            item.RemoveDate = DateTime.Now.AddHours(item.Term);
            user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            item.Owner = user;
            db.Items.Add(item);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Item item = await db.Items.Include(p => p.Owner).Include(b => b.Bets).FirstOrDefaultAsync(p => p.Id == id);                
                if (item != null)
                    return View(item);
            }
            return NotFound();
        }

        // UPDATE
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Item item = await db.Items.FirstOrDefaultAsync(p => p.Id == id);
                if (item != null)
                    return View(item);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Item item)
        {
            string html = GetImageFromGoogle.GetHtmlCode(item.Name);
            item.ImgUrl = GetImageFromGoogle.GetUrls(html);
            item.RemoveDate = DateTime.Now.AddHours(item.Term);
            db.Items.Update(item);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //DELETE

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Item item = await db.Items.FirstOrDefaultAsync(p => p.Id == id);
                if (item != null)
                    return View(item);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Item item = new Item { Id = id.Value };
                db.Entry(item).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
