using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkelbimuSvetaine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkelbimuSvetaine.Security;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SkelbimuSvetaine.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ld1_gynimasContext _context;
        private readonly string salt = BCryptNet.GenerateSalt(13);
        public UserController(ld1_gynimasContext context)
        {
            _context = context;          
        }


        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber, string currentFilter)
        {

            var valueFormSession = HttpContext.Session.GetInt32("keyword") ?? default;
            HttpContext.Session.SetInt32("keyword", valueFormSession + 1);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CitySortParm"] = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.Users select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Username.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Username);  
                    break;
                case "city_desc":
                    students = students.OrderByDescending(s => s.Miestas);
                    break;
            }

            int pageSize = 6;
            return View(await PaginatedList<User>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));

            //return View(await students.AsNoTracking().ToListAsync());

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username, Password, ConfirmPassword, Phone, Email, Icon, Miestas, Role")] User user)        
        {
            if(_context.Users.Any(x => x.Username == user.Username))
                throw new Exception("Vardas '" + user.Username + "' jau užimtas");
            try
            {
                if(ModelState.IsValid)
                {
                    user.Password = BCryptNet.HashPassword(user.Password, salt);
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Nepavyko išsaugoti pakeitimų, jei vis dar kyla problema susisiekite su administratoriumi");
            }
            return View(User);

        }

        [HttpGet]
        public ActionResult Create()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            var student = await _context.Users.FindAsync(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Users.Remove(student);
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
            return View(_context.Users.Find(id));
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<User>(
                studentToUpdate,
                "",
                s => s.Username, s => s.Password, s=> s.ConfirmPassword, s => s.Email, s => s.Phone, s => s.Miestas))
            {
                try
                {
                    studentToUpdate.Password = BCryptNet.HashPassword(studentToUpdate.Password, salt);
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
            return View(_context.Users.Find(id));
        }

    

    }
}
