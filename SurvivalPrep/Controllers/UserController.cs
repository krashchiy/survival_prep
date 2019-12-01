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
            return RedirectToAction("Highscores");
        }

        public IActionResult Highscores()
        {
            ViewData["Disasters"] = _db.Disasters.ToList();
            ViewData["Selected"] = "_All";

            List<ApplicationUser> usersList = _userManager.Users.Include(u => u.OwnedItems).ThenInclude(i => i.Item).ToList();
            usersList.Sort();
            return View(usersList);
        }

        public async Task<IActionResult> DisasterScores(String? disaster)
        {
            if(disaster == null)
            {
                return NotFound();
            }

            Disaster disastervar = await _db.Disasters
                .Include(d=>d.ItemDisasters)
                .FirstOrDefaultAsync(o => o.Name == disaster);

            if(disastervar == null)
            {
                return NotFound();
            }

            ViewData["Disasters"] = _db.Disasters.ToList();

            List<ApplicationUser> usersList = _userManager.Users.Include(u => u.OwnedItems)
                .ThenInclude(i => i.Item)
                .ThenInclude(it=>it.ItemDisasters)
                .ToList();
            List<Tuple<String, int>> userScores = new List<Tuple<string, int>>();

            foreach(ApplicationUser user in usersList)
            {
                userScores.Add(new Tuple<string, int>(user.UserName, user.DisasterScore(disastervar)));
            }

            userScores.Sort((a, b) =>b.Item2.CompareTo(a.Item2));

            ViewData["Disaster"] = disastervar.Name;
            ViewData["UserScores"] = userScores;
            ViewData["Selected"] = disastervar.Name;

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