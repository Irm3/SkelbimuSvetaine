using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkelbimuSvetaine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkelbimuSvetaine.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly ld1_gynimasContext _context;
        public RatingController(ld1_gynimasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.Ratings.Include(d => d.User).Include(d => d.Product).ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Value, UserId, ProductId")] Rating rating)
        {
            try
            {
            if (ModelState.IsValid)
            {
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
            catch
            {
                ModelState.AddModelError("", "Nepavyko išsaugoti pakeitimų, jei vis dar kyla problema susisiekite su administratoriumi");
            }
            return View(rating);

        }

        [HttpGet]
        public ActionResult Create()
        {
            Rating rating = new Rating();

            return View(rating);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            var student = await _context.Ratings.FindAsync(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Ratings.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(_context.Ratings.Find(id));
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Ratings.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Rating>(
                studentToUpdate,
                "",
                s => s.Value))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(_context.Ratings.Find(id));
        }
    }
}
