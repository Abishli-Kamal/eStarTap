using eStartap.DAL;
using eStartap.Models;
using eStartap.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eStartap.Areas.eStartapAdmin.Controllers
{
    [Area("eStartapAdmin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Card> cards = await _context.Cards.ToListAsync();
            return View(cards);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Card card)
        {
            if (!ModelState.IsValid) return View();
            if (card == null) return View();
            if (card.Photo != null)
            {

                if (card.Photo.Length < 1024 * 1024 && card.Photo.ContentType.Contains("image"))
                {
                    card.Image = await card.Photo.FileCreate(_env.WebRootPath, @"assets\img");
                    await _context.Cards.AddAsync(card);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "uzunlugu duzgun qeyd edin");
                    return View();
                }

            }
            else
            {
                ModelState.AddModelError("", "Image secin");
                return View();
            }



        }
        public async Task<IActionResult> Edit(int id)
        {
            Card cards = await _context.Cards.FirstOrDefaultAsync(s => s.Id == id);
            if (cards == null) return View();
            return View(cards);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, Card card)
        {
            Card existedcard = await _context.Cards.FirstOrDefaultAsync(card => card.Id == id);

            if (existedcard.Id != card.Id) return View();


            if (card.Photo != null)
            {
                if (card.Photo.ContentType.Contains("image") && card.Photo.Length < 1024 * 1024)
                {
                    existedcard.Title = card.Title;
                    existedcard.Date = card.Date;
                    existedcard.Photo = card.Photo;
                    existedcard.Image = await card.Photo.FileCreate(_env.WebRootPath, @"assets\img");

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Duzgun melumatlar daxil edin");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "File secin!");
                return View();
            }


        }


        public async Task<IActionResult> Delete(int id)
        {
            Card cards = await _context.Cards.FirstOrDefaultAsync(s => s.Id == id);
            if (cards == null) return View();
            return View(cards);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteCard(int id)
        {
            Card cards = await _context.Cards.FirstOrDefaultAsync(s => s.Id == id);
            if (cards == null) return View();


            _context.Cards.Remove(cards);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
