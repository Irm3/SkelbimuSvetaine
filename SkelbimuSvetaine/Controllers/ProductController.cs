using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkelbimuSvetaine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkelbimuSvetaine.Controllers
{
    public class ProductController : Controller
    {
        private readonly ld1_gynimasContext _context;
        public ProductController(ld1_gynimasContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Index() => View(await _context.Products.Include(d => d.User).Include(d => d.Category).ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prekes = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (prekes == null)
            {
                return NotFound();
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
    }
}
