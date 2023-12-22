using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Data;
using QLSV.Models;
using QLSV.Models.Process;

namespace QLSV.Controllers
{
    public class LopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LopController(ApplicationDbContext context)
        {
            _context = context;
        }

        private ExcelProcess _excelPro = new ExcelProcess();
        // Search 
        public async Task<IActionResult> Index(string searchString)
        {
             
            var Lop = from m in _context.Lop
                select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Lop = Lop.Where(s => s. Malop!.Contains(searchString));
                }
            return View(await Lop.ToListAsync());
        }
         // ACTION UPLOAD
          public  IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file!=null)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    if (fileExtension != ".xls" && fileExtension != ".xlsx")
                    {
                        ModelState.AddModelError("", "Please choose excel file to upload!");
                    }
                    else
                    {
                        //rename file when upload to server
                        var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", "File" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond + fileExtension);
                        var fileLocation = new FileInfo(filePath).ToString();
                        if (file.Length > 0)
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                //save file to server
                                await file.CopyToAsync(stream);
                                //read data from file and write to database
                                var dt = _excelPro.ExcelToDataTable(fileLocation);
                                for(int i = 0; i < dt.Rows.Count; i++)
                                {
                                   var lop = new Lop();
                                    lop.Malop = dt.Rows[i][0].ToString();
                                    lop.Tenlop = dt.Rows[i][1].ToString();
                                    _context.Add(lop);

                                }
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
            
            return View();
        }

        // GET: Lop/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lop = await _context.Lop
                .FirstOrDefaultAsync(m => m.Malop == id);
            if (lop == null)
            {
                return NotFound();
            }

            return View(lop);
        }

        // GET: Lop/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Malop,Tenlop")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lop);
        }

        // GET: Lop/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lop = await _context.Lop.FindAsync(id);
            if (lop == null)
            {
                return NotFound();
            }
            return View(lop);
        }

        // POST: Lop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Malop,Tenlop")] Lop lop)
        {
            if (id != lop.Malop)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LopExists(lop.Malop))
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
            return View(lop);
        }

        // GET: Lop/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lop = await _context.Lop
                .FirstOrDefaultAsync(m => m.Malop == id);
            if (lop == null)
            {
                return NotFound();
            }

            return View(lop);
        }

        // POST: Lop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lop = await _context.Lop.FindAsync(id);
            if (lop != null)
            {
                _context.Lop.Remove(lop);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LopExists(string id)
        {
            return _context.Lop.Any(e => e.Malop == id);
        }
    }
}
