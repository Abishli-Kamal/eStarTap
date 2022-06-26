using eStartap.DAL;
using eStartap.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eStartap.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM model = new HomeVM
            {
                HomePages = await _context.HomePages.FirstOrDefaultAsync(),
                Cards = await _context.Cards.ToListAsync(),
            };
            return View(model); 
        }
    }
}
