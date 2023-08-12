using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SweetAndSavory.Controllers
{
    public class HomeController : Controller
    {
        private readonly SweetAndSavoryContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager, SweetAndSavoryContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [HttpGet("/")]
        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated){
            Flavor[] flavors = _db.Flavors.ToArray();
            Dictionary<string, object[]> model = new Dictionary<string, object[]>();
            model.Add("flavors", flavors);
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser != null)
            {
                Treat[] treats = _db.Treats
                    // .Where(entry => entry.User.Id == currentUser.Id)
                    .ToArray();
                model.Add("treats", treats);
            }
            return View(model);
            }
            else{
                return View(_db.Flavors.ToList());
            }
        }
    }
}