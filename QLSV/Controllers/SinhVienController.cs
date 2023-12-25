using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using QLSV.Data;
using QLSV.Models;
using QLSV.Models.Process;

namespace QLSV.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SinhVienController(ApplicationDbContext context)
        {
            _context = context;
        }
        private ExcelProcess _excelPro = new ExcelProcess();

        // GET: SinhVien
        //Action Search SV
        public async Task<IActionResult> Index(string searchString)
        {
             
            var SinhVien = from m in _context.SinhVien
                select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                SinhVien = SinhVien.Where(s => s. MaSV!.Contains(searchString));
                }
            return View(await SinhVien.ToListAsync());
        }
        // ACTION UPLOAD
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                                 var dt = _excelPro.ExcelToDataTable(fileLocation);
                                for(int i = 0; i < dt.Rows.Count; i++)
                                {
                                    var sv = new SinhVien();
                                    sv.MaSV = dt.Rows[i][0].ToString();
                                    sv.Hovaten = dt.Rows[i][1].ToString();
                                    sv.DiaChi = dt.Rows[i][2].ToString();
                                    sv.Malop = dt.Rows[i][3].ToString();
                                    sv.Makhoa = dt.Rows[i][4].ToString();
                                    _context.Add(sv);
                                }
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
                return View();
        }
         public IActionResult Download()
        {
            var fileName = "SinhVienList.xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                excelWorksheet.Cells["A1"].Value = "MaSV";
                excelWorksheet.Cells["B1"].Value = "Hovaten";
                excelWorksheet.Cells["C1"].Value = "DiaChi";
                excelWorksheet.Cells["D1"].Value = "Malop";
                excelWorksheet.Cells["E1"].Value = "MaKhoa";
                var SinhVienList = _context.SinhVien.ToList();
                excelWorksheet.Cells["A2"].LoadFromCollection(SinhVienList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",fileName);
            }
        }
        // GET: SinhVien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhVien
                .Include(s => s.Khoa)
                .Include(s => s.Lop)
                .FirstOrDefaultAsync(m => m.MaSV == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // GET: SinhVien/Create
        public IActionResult Create()
        {
            ViewData["Makhoa"] = new SelectList(_context.Khoa, "Makhoa", "Makhoa");
            ViewData["Malop"] = new SelectList(_context.Lop, "Malop", "Malop");
            return View();
        }

        // POST: SinhVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSV,Hovaten,DiaChi,Malop,Makhoa")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sinhVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Makhoa"] = new SelectList(_context.Khoa, "Makhoa", "Makhoa", sinhVien.Makhoa);
            ViewData["Malop"] = new SelectList(_context.Lop, "Malop", "Malop", sinhVien.Malop);
            return View(sinhVien);
        }

        // GET: SinhVien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhVien.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            ViewData["Makhoa"] = new SelectList(_context.Khoa, "Makhoa", "Makhoa", sinhVien.Makhoa);
            ViewData["Malop"] = new SelectList(_context.Lop, "Malop", "Malop", sinhVien.Malop);
            return View(sinhVien);
        }

        // POST: SinhVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSV,Hovaten,DiaChi,Malop,Makhoa")] SinhVien sinhVien)
        {
            if (id != sinhVien.MaSV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sinhVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhVienExists(sinhVien.MaSV))
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
            ViewData["Makhoa"] = new SelectList(_context.Khoa, "Makhoa", "Makhoa", sinhVien.Makhoa);
            ViewData["Malop"] = new SelectList(_context.Lop, "Malop", "Malop", sinhVien.Malop);
            return View(sinhVien);
        }

        // GET: SinhVien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhVien
                .Include(s => s.Khoa)
                .Include(s => s.Lop)
                .FirstOrDefaultAsync(m => m.MaSV == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // POST: SinhVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sinhVien = await _context.SinhVien.FindAsync(id);
            if (sinhVien != null)
            {
                _context.SinhVien.Remove(sinhVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SinhVienExists(string id)
        {
            return _context.SinhVien.Any(e => e.MaSV == id);
        }
    }
}
