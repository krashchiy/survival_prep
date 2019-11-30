using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurvivalPrep.DBModels;

namespace SurvivalPrep.Controllers
{
    public class UserController : Controller
    {
        private readonly PrepContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(PrepContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Redirect to High Scores page when built
            return View();
        }

        public async Task<IActionResult> Details(String? username)
        {
            if(username == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users.Include(u => u.OwnedItems)
                .ThenInclude(i=>i.Item)
                .ThenInclude(it=>it.ItemDisasters)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if(user == null)
            {
                return NotFound();
            }

            var tuple = new Tuple<ApplicationUser, IEnumerable<Disaster>>(user, await _db.Disasters.Include(d => d.ItemDisasters).ToListAsync());

            return View(tuple);
        }
    }
}