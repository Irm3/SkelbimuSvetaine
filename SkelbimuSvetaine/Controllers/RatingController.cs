﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkelbimuSvetaine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkelbimuSvetaine.Controllers
{
    public class RatingController : Controller
    {
        private readonly ld1_gynimasContext _context;
        public RatingController(ld1_gynimasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.Ratings.Include(d => d.User).Include(d => d.Product).ToListAsync());
    }
}
