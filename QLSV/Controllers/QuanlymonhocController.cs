using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Data;
using QLSV.Models;

namespace QLSV.Controllers
{
    public class QuanlymonhocController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanlymonhocController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Quanlymonhoc
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quanlymonhoc.ToListAsync());
        }

        // GET: Quanlymonhoc/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quanlymonhoc = await _context.Quanlymonhoc
                .FirstOrDefaultAsync(m => m.Mamonhoc == id);
            if (quanlymonhoc == null)
            {
                return NotFound();
            }

            return View(quanlymonhoc);
        }

        // GET: Quanlymonhoc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quanlymonhoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mamonhoc,Tenmonhoc")] Quanlymonhoc quanlymonhoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quanlymonhoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quanlymonhoc);
        }

        // GET: Quanlymonhoc/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quanlymonhoc = await _context.Quanlymonhoc.FindAsync(id);
            if (quanlymonhoc == null)
            {
                return NotFound();
            }
            return View(quanlymonhoc);
        }

        // POST: Quanlymonhoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mamonhoc,Tenmonhoc")] Quanlymonhoc quanlymonhoc)
        {
            if (id != quanlymonhoc.Mamonhoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quanlymonhoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuanlymonhocExists(quanlymonhoc.Mamonhoc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quanlymonhoc);
        }

        // GET: Quanlymonhoc/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quanlymonhoc = await _context.Quanlymonhoc
                .FirstOrDefaultAsync(m => m.Mamonhoc == id);
            if (quanlymonhoc == null)
            {
                return NotFound();
            }

            return View(quanlymonhoc);
        }

        // POST: Quanlymonhoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var quanlymonhoc = await _context.Quanlymonhoc.FindAsync(id);
            if (quanlymonhoc != null)
            {
                _context.Quanlymonhoc.Remove(quanlymonhoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuanlymonhocExists(string id)
        {
            return _context.Quanlymonhoc.Any(e => e.Mamonhoc == id);
        }
    }
}
