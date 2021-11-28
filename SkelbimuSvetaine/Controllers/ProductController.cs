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

        public async Task<IActionResult> Index() => View(await _context.Products.Include(d => d.User).Include(d => d.Category).ToListAsync());

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
    }
}
