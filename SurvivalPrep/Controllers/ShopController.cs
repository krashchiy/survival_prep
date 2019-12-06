using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurvivalPrep.DBModels;

namespace SurvivalPrep.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly PrepContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShopController(PrepContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Backend of Ajax call to display details for selected item on store page
        public async Task<IActionResult> ShowDetails(int id)
        {
            //Get selected item
            var item = await _context.Items.FindAsync(id + 1);
            //Get current user
            var user_id = _userManager.GetUserId(User);

            //If user owns selected item display the amount owned, otherwise display 0
            int current;
            var curItem = _context.ItemInstances.Where(i => i.ItemId == item.ID && i.ApplicationUserID == user_id).FirstOrDefault();
            if (curItem == null)
            {
                current = 0;
            }
            else
            {
                current = curItem.Quantity;
            }

            return Json(
                new
                {
                    success = true,
                    name = item.Name,
                    score = item.Score.ToString(),
                    owned = current,
                    cost = item.Cost.ToString()
                });
        }

        //Backend of Ajax call to buy item
        public async Task<IActionResult> BuyItem(int id, int quantity)
        {
            //Get selected item
            var item = await _context.Items.FindAsync(id + 1);
            //Get current user
            var user_id = _userManager.GetUserId(User);
            var user = _context.Users.FirstOrDefault(s => s.Id == user_id);

            //If user has enough monney
            if (user.Money < item.Cost * quantity)
            {
                return BadRequest(new JsonResult(new { success = false }));
            }
            else
            {
                //Subtract required money from total money and add bought items to user's owned items
                user.Money = user.Money - item.Cost * quantity;

                //If user doesn't own item, create new instance of item, otherwise as quantity bought to existing instance
                var curItem = _context.ItemInstances.Where(i => i.ItemId == item.ID && i.ApplicationUserID == user_id).FirstOrDefault();
                if (curItem == null)
                {
                    user.OwnedItems = new List<ItemInstance>
                    {
                        new ItemInstance { ApplicationUser=user, ItemId=item.ID, Quantity=quantity }
                    };
                }
                else
                {
                    curItem.Quantity = curItem.Quantity + quantity;
                }
                _context.SaveChanges();
            }

            return Json(
                new
                {
                    success = true,
                    money = user.Money
                });
        }

        // GET: Shop
        public async Task<IActionResult> Index()
        {
            var id = _userManager.GetUserId(User);
            var user = _context.Users.FirstOrDefault(s => s.Id == id);
            if (user != null)
            {
                ViewData["Money"] = user.Money;
            }

            var items = from s in _context.Items
                          select s;

            items = items.Include(i => i.ItemDisasters).ThenInclude(it => it.Disaster);

            return View(await items.AsNoTracking().ToListAsync());
        }

    }
}
