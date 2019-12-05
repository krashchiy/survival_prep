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

        public async Task<IActionResult> ShowDetails(int id)
        {
            var item = await _context.Items.FindAsync(id + 1);
            var user_id = _userManager.GetUserId(User);
            var user = _context.Users.FirstOrDefault(s => s.Id == user_id);

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

        public async Task<IActionResult> BuyItem(int id, int quantity)
        {
            var item = await _context.Items.FindAsync(id + 1);
            var user_id = _userManager.GetUserId(User);
            var user = _context.Users.FirstOrDefault(s => s.Id == user_id);

            if (user.Money < item.Cost * quantity)
            {
                return BadRequest(new JsonResult(new { success = false }));
            }
            else
            {
                user.Money = user.Money - item.Cost * quantity;

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
