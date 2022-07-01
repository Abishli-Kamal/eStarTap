using eStartap.DAL;
using eStartap.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eStartap.Areas.eStartapAdmin.Controllers
{
    [Area("eStartapAdmin")]
    public class UsersController : Controller
    {
        
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public UsersController(UserManager<AppUser> userManager,AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> User()
        {
            List<AppUser> users = await _context.Users.ToListAsync();
            return View(users);
        }
    }
}
