using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SweetAndSavory.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using SweetAndSavory.ViewModels;

namespace SweetAndSavory.Controllers
{
    [Authorize]
    public class TreatsController : Controller
    {
        private readonly SweetAndSavoryContext _db;
        private readonly UserManager<Account> _userManager;

        public TreatsController(UserManager<Account> userManager, SweetAndSavoryContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Treats.ToList());
        }

        public ActionResult Details(int id)
        {
            Treat thisTreat = _db.Treats
            .Include(treat => treat.JoinEntities)
            .ThenInclude(join => join.Flavor)
            .FirstOrDefault(thisTreat => thisTreat.TreatId == id);
            return View(thisTreat);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Treat treat)
        {
            _db.Treats.Add(treat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddFlavor(int id)
        {
            Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
            ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
            return View(thisTreat);
        }
        public ActionResult AddFlavor(Treat treat, int flavorId)
        {
            FlavorTreat joinEntity = _db.FlavorTreats.FirstOrDefault(join => join.FlavorId == flavorId && join.TreatId == treat.TreatId);
            if (joinEntity == null && flavorId != 0)
            {
                _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = flavorId, TreatId = treat.TreatId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = treat.TreatId });

        }
    }
}