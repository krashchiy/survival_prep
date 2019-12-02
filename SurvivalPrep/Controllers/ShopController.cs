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
    public class ShopController : Controller
    {
        private readonly PrepContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShopController(PrepContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
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

        // GET: Shop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Shop/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shop/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Score,Cost")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Shop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Shop/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Score,Cost")] Item item)
        {
            if (id != item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Shop/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Shop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ID == id);
        }
    }
}
