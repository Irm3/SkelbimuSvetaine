using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkelbimuSvetaine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkelbimuSvetaine.Controllers
{
    public class CommentController : Controller
    {
        private readonly ld1_gynimasContext _context;
        public CommentController(ld1_gynimasContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Index() => View(await _context.Comments.Include(d => d.User).Include(d => d.Product).ToListAsync());

        public async Task<IActionResult> Index()
        {

            var kom = await _context.Comments.Include(d => d.User).Include(d => d.Product).ToListAsync();
            return View(kom);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Message, UserId, ProductId")] Comment comment)
        {
            //try
            //{
                if (ModelState.IsValid)
                {
                    _context.Add(comment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            //}
            //catch
            //{
            //    ModelState.AddModelError("", "Nepavyko išsaugoti pakeitimų, jei vis dar kyla problema susisiekite su administratoriumi");
            //}
            return View(comment);

        }

        [HttpGet]
        public ActionResult Create()
        {
            Comment comment = new Comment();
 
            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            var student = await _context.Comments.FindAsync(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Comments.Remove(student);
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
            return View(_context.Comments.Find(id));
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Comments.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Comment>(
                studentToUpdate,
                "",
                s => s.Message))
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
            return View(_context.Comments.Find(id));
        }

    }
}
