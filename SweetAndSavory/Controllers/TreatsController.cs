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
            return View(_db.Tags.ToList());
        }
    }
}