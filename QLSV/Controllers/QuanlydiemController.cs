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
using X.PagedList;

namespace QLSV.Controllers
{
    public class QuanlydiemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanlydiemController(ApplicationDbContext context)
        {
            _context = context;
        }

         private ExcelProcess _excelPro = new ExcelProcess();

     
        public async Task<IActionResult> Index(int? page, int? PageSize)
        {
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="3", Text="3"},
                new SelectListItem() { Value="5", Text="5"},
                new SelectListItem() { Value="10", Text="10"},
                new SelectListItem() { Value="15", Text="15"},
                new SelectListItem() { Value="25", Text="25"},
                new SelectListItem() { Value="50", Text="50"},
            };
            int pagesize = (PageSize ?? 3);
            ViewBag.psize = pagesize;
            var model = _context.Quanlydiem.ToList().ToPagedList(page ?? 1, pagesize);
            return View(model);
        }
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
                                    var qld = new Quanlydiem();
                                    qld.Sothutu = Convert.ToInt32(dt.Rows[i][0].ToString());
                                    qld.MaSV = dt.Rows[i][1].ToString();
                                    qld.TenSV = dt.Rows[i][2].ToString();
                                    qld.Mamonhoc = dt.Rows[i][3].ToString();
                                    qld.DiemMH = dt.Rows[i][4].ToString();
                                    _context.Add(qld);
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
            var fileName = "QuanlydiemList.xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                excelWorksheet.Cells["A1"].Value = "Sothutu";
                excelWorksheet.Cells["B1"].Value = "MaSV";
                excelWorksheet.Cells["C1"].Value = "TenSV";
                excelWorksheet.Cells["D1"].Value = "Mamonhoc";
                excelWorksheet.Cells["E1"].Value = "DiemMH";
                var QuanlydiemList = _context.Quanlydiem.ToList();
                excelWorksheet.Cells["A2"].LoadFromCollection(QuanlydiemList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",fileName);
            }
        }


        // GET: Quanlydiem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quanlydiem = await _context.Quanlydiem
                .Include(q => q.Masv)
                .Include(q => q.Monhoc)
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
            ViewData["MaSV"] = new SelectList(_context.SinhVien, "MaSV", "MaSV");
            ViewData["Mamonhoc"] = new SelectList(_context.Quanlymonhoc, "Mamonhoc", "Mamonhoc");
            return View();
        }

        // POST: Quanlydiem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sothutu,MaSV,TenSV,Mamonhoc,DiemMH")] Quanlydiem quanlydiem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quanlydiem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaSV"] = new SelectList(_context.SinhVien, "MaSV", "MaSV", quanlydiem.MaSV);
            ViewData["Mamonhoc"] = new SelectList(_context.Quanlymonhoc, "Mamonhoc", "Mamonhoc", quanlydiem.Mamonhoc);
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
            ViewData["MaSV"] = new SelectList(_context.SinhVien, "MaSV", "MaSV", quanlydiem.MaSV);
            ViewData["Mamonhoc"] = new SelectList(_context.Quanlymonhoc, "Mamonhoc", "Mamonhoc", quanlydiem.Mamonhoc);
            return View(quanlydiem);
        }

        // POST: Quanlydiem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sothutu,MaSV,TenSV,Mamonhoc,DiemMH")] Quanlydiem quanlydiem)
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
            ViewData["MaSV"] = new SelectList(_context.SinhVien, "MaSV", "MaSV", quanlydiem.MaSV);
            ViewData["Mamonhoc"] = new SelectList(_context.Quanlymonhoc, "Mamonhoc", "Mamonhoc", quanlydiem.Mamonhoc);
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
                .Include(q => q.Masv)
                .Include(q => q.Monhoc)
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
