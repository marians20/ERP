using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Erp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Erp.Controllers
{
    public abstract class MyBaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public MyBaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected void AddGendersSelectList(object selectedValue = null)
        {
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name", selectedValue);
        }
    }
}