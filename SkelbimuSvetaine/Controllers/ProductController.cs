using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkelbimuSvetaine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SkelbimuSvetaine.Controllers
{
    public class ProductController : Controller
    {
        private readonly ld1_gynimasContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(ld1_gynimasContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        //public async Task<IActionResult> Index() => View(await _context.Products.Include(d => d.User).Include(d => d.Category).ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prekes = await _context.Products.Include(m => m.User).Include(m => m.Category).Include(m => m.Comments).FirstOrDefaultAsync(m => m.Id == id);
            if (prekes == null)
            {
                return NotFound();
            }

            var comments = _context.Comments.Where(a => a.ProductId == id).ToList();
            foreach(Comment c in comments)
            {
                var user = await _context.Users.FirstOrDefaultAsync(z => z.Id == c.UserId);
                c.User.Username = user.Username;
                prekes.Comments.Add(c);
            }

            var ratings = _context.Ratings.Where(a => a.ProductId == id).ToList();
            foreach (Rating r in ratings)
            {
                var user = await _context.Users.FirstOrDefaultAsync(z => z.Id == r.UserId);
                r.User.Username = user.Username;
                prekes.Ratings.Add(r);
            }

            return View(prekes);
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber, string currentFilter)
        {

            var valueFormSession = HttpContext.Session.GetInt32("keyword") ?? default;
            HttpContext.Session.SetInt32("keyword", valueFormSession + 1);

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var students = from s in _context.Products select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString));
            }

            int pageSize = 20;
            return View(await PaginatedList<Product>.CreateAsync(students.AsNoTracking().Include(d => d.User), pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Description, Price, Image, UserId, CategoryId, CreatedTimestamp")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Nepavyko išsaugoti pakeitimų, jei vis dar kyla problema susisiekite su administratoriumi");
            }
            return View(product);

        }

        [HttpGet]
        public ActionResult Create()
        {        
            Product product = new Product();
            //PopulateSelections(product);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            var student = await _context.Products.FindAsync(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Products.Remove(student);
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
            return View(_context.Products.Find(id));
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Product>(
                studentToUpdate,
                "",
                s => s.Name, s => s.Description, s => s.Price, s => s.Image, s => s.Category))
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
            return View(_context.Products.Find(id));
        }

    }
}
