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
    public class QuanlydiemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanlydiemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Quanlydiem
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quanlydiem.ToListAsync());
        }

        // GET: Quanlydiem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quanlydiem = await _context.Quanlydiem
                .FirstOrDefaultAsync(m => m.Sothutu == id);
            if (quanlydiem == null)
            {
                return NotFound();
            }

            return View(quanlydiem);
        }

        // GET: Quanlydiem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quanlydiem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sothutu,MaSV,TenSV,Tenmonhoc,Diem")] Quanlydiem quanlydiem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quanlydiem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quanlydiem);
        }

        // GET: Quanlydiem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quanlydiem = await _context.Quanlydiem.FindAsync(id);
            if (quanlydiem == null)
            {
                return NotFound();
            }
            return View(quanlydiem);
        }

        // POST: Quanlydiem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sothutu,MaSV,TenSV,Tenmonhoc,Diem")] Quanlydiem quanlydiem)
        {
            if (id != quanlydiem.Sothutu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quanlydiem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuanlydiemExists(quanlydiem.Sothutu))
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
            return View(quanlydiem);
        }

        // GET: Quanlydiem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quanlydiem = await _context.Quanlydiem
                .FirstOrDefaultAsync(m => m.Sothutu == id);
            if (quanlydiem == null)
            {
                return NotFound();
            }

            return View(quanlydiem);
        }

        // POST: Quanlydiem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quanlydiem = await _context.Quanlydiem.FindAsync(id);
            if (quanlydiem != null)
            {
                _context.Quanlydiem.Remove(quanlydiem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuanlydiemExists(int id)
        {
            return _context.Quanlydiem.Any(e => e.Sothutu == id);
        }
    }
}
