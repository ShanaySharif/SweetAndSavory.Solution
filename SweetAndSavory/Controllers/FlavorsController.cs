using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SweetAndSavory.Controllers
{
    [Authorize]
    public class FlavorsController : Controller
    {
        private readonly SweetAndSavoryContext _db;
        private readonly UserManager<Account> _userManager;
        public FlavorsController(UserManager<Account> userManager, SweetAndSavoryContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<ActionResult> Index()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Account currentUser = await _userManager.FindByIdAsync(userId);
            List<Flavor> userFlavors = _db.Flavors
              .Include(SweetAndSavory => SweetAndSavory.JoinEntities)
              .ThenInclude(join => join.Treat)
              .ToList();
            return View(userFlavors);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Flavor flavor, int FlavorId)
        {
            if (!ModelState.IsValid)
            {
                return View(flavor);
            }
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Account currentUser = await _userManager.FindByIdAsync(userId);
                flavor.User = currentUser;
                _db.Flavors.Add(flavor);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
        }
        public ActionResult Details(int id)
        {
            Flavors thisFlavor = _db.Flavors
            .Include(flavor => flavor.JoinEntities)
            .ThenInclude(join => join.Treat)
            .FirstOrDefault(thisFlavor => thisFlavor.FlavorId == id);
            return View(thisFlavor);
        }

        public ActionResult Edit(int id)
        {
            Flavor thisFlavor = _db.Flavors
            .FirstOrDefault(flavor => flavor.FlavorId == id);
            return View(thisFlavor);
        }

        [HttpPost]
        public ActionResult Edit(Flavor flavor)
        {
            _db.Flavors.Update(flavor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Flavor thisFlavor = _dbFlavors.FirstOrDefault(flavor = FlavorsController.FlavorId == id);
            return View(thisFlavor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
            _db.Flavors.Remove(thisFlavor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> AddTreat(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Account currentUser = await _userManager.FindByIdAsync(userId);
            List<Treat> userTreats = _db.Treats
              .Include(flavors => flavors.JoinEntities)
              .ToList();
            Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
            ViewBag.TreatId = new SelectList(userTreats, "TreatId", "TreatName");
            return View(thisRecipe);
        }
        [HttpPost]
        public ActionResult AddTreat(Flavor flavor, int treatId)
        {
#nullable enable
            FlavorTreat? joinEntity = _db.FlavorTreats.FirstOrDefault(join => (join.TreatId == treatId && join.FlavorId == flavor.FlavorId));
#nullable disable
            if (joinEntity == null && treatId != 0)
            {
                _db.FlavorTreats.Add(new FlavorTreat() { TreatId = treatId, FlavorId = flavor.FlavorId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = flavor.FlavorId });
        }
        [HttpPost]
        public ActionResult DeleteJoin(int joinId)
        {
            FlavorTreat entry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
            _db.FlavorTreats.Remove(entry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}