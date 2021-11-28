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

            var kom = await _context.Comments.ToListAsync();
            if (kom.Count == 0)
            {
                return Content("Tuščia lentelė");
            }
            else
            return View(kom);
        }

    }
}
