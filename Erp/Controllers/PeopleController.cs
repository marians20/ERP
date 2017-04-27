using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models.People;
using Sakura.AspNetCore;

namespace Erp.Controllers
{
    public class PeopleController : MyBaseController
    {

        public PeopleController(ApplicationDbContext context)
            :base(context)
        {
            
        }

        // GET: People
        public async Task<IActionResult> Index(string sortOrder, int? page, int? pageSize)
        {
            IQueryable<Person> items = _context.People
                .Include(p => p.Gender);
            //AddFieldNames<Person>();
            //AddSortParameters<Person>(sortOrder);
            ViewData["FirstNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewData["LastNameSortParam"] = sortOrder == "lastname" ? "lastname_desc" : "lastname";
            switch(sortOrder)
            {
                case "firstname":
                    items = items.OrderBy(x => x.FirstName);
                    break;
                case "firstname_desc":
                    items = items.OrderByDescending(x => x.FirstName);
                    break;
                case "lastname":
                    items = items.OrderBy(x => x.LastName);
                    break;
                case "lastname_desc":
                    items = items.OrderByDescending(x => x.LastName);
                    break;
            }
            return View(Paginated(items, page, pageSize));
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Gender)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            AddGendersSelectList();
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,SocialId,BirthDate,GenderId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Gender)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            AddGendersSelectList(person.GenderId);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,SocialId,BirthDate,GenderId")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Gender)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.SingleOrDefaultAsync(m => m.Id == id);
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
