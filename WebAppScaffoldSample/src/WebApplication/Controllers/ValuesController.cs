using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ValuesController : Controller
    {
        private readonly SQLiteDbContext _context;

        public ValuesController(SQLiteDbContext context)
        {
            _context = context;    
        }

        // GET: Values
        public async Task<IActionResult> Index()
        {
            return View(await _context.Values.ToListAsync());
        }

        // GET: Values/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var value = await _context.Values.SingleOrDefaultAsync(m => m.Id == id);
            if (value == null)
            {
                return NotFound();
            }

            return View(value);
        }

        // GET: Values/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Values/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Value value)
        {
            if (ModelState.IsValid)
            {
                _context.Add(value);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(value);
        }

        // GET: Values/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var value = await _context.Values.SingleOrDefaultAsync(m => m.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return View(value);
        }

        // POST: Values/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Value value)
        {
            if (id != value.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(value);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValueExists(value.Id))
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
            return View(value);
        }

        // GET: Values/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var value = await _context.Values.SingleOrDefaultAsync(m => m.Id == id);
            if (value == null)
            {
                return NotFound();
            }

            return View(value);
        }

        // POST: Values/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var value = await _context.Values.SingleOrDefaultAsync(m => m.Id == id);
            _context.Values.Remove(value);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ValueExists(int id)
        {
            return _context.Values.Any(e => e.Id == id);
        }
    }
}
