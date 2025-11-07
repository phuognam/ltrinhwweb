using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mpn_231230846_de01.Data;
using mpn_231230846_de01.Models;

namespace mpn_231230846_de01.Controllers
{
    public class MpnComputerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public MpnComputerController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> MpnIndex()
        {
            var list = await _context.MpnComputer.ToListAsync();
            return View(list);
        }
        public async Task<IActionResult> MpnDetails(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.MpnComputer.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }
        public IActionResult MpnCreate() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MpnCreate(MpnComputer model)
        {
            if (ModelState.IsValid)
            {
                // xử lý ảnh
                if (model.ImageFile != null)
                {
                    var allowedExt = new[] { ".jpg", ".jpeg", ".png", ".gif", ".tiff" };
                    var ext = Path.GetExtension(model.ImageFile.FileName).ToLower();
                    if (!allowedExt.Contains(ext))
                    {
                        ModelState.AddModelError("ImageFile", "Chỉ chấp nhận .jpg .png .gif .tiff");
                        return View(model);
                    }
                    if (model.ImageFile.Length > 5 * 1024 * 1024) // giới hạn 5MB
                    {
                        ModelState.AddModelError("ImageFile", "Ảnh phải nhỏ hơn 5MB");
                        return View(model);
                    }

                    var uploads = Path.Combine(_env.WebRootPath, "images");
                    if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                    var filename = $"{Guid.NewGuid()}{ext}";
                    var filepath = Path.Combine(uploads, filename);
                    using var stream = new FileStream(filepath, FileMode.Create);
                    await model.ImageFile.CopyToAsync(stream);
                    model.mpnComImage = $"/images/{filename}";
                }

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MpnIndex));
            }
            return View(model);
        }
        public async Task<IActionResult> MpnEdit(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.MpnComputer.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HvtEdit(int id, MpnComputer model)
        {
            if (id != model.mpnComId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    var dbItem = await _context.MpnComputer.FindAsync(id);
                    if (dbItem == null) return NotFound();

                    dbItem.mpnComName = model.mpnComName;
                    dbItem.mpnComPrice = model.mpnComPrice;
                    dbItem.mpnComStatus = model.mpnComStatus;

                    if (model.ImageFile != null)
                    {
                        var ext = Path.GetExtension(model.ImageFile.FileName).ToLower();
                        var uploads = Path.Combine(_env.WebRootPath, "images");
                        if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                        var filename = $"{Guid.NewGuid()}{ext}";
                        var filepath = Path.Combine(uploads, filename);
                        using var stream = new FileStream(filepath, FileMode.Create);
                        await model.ImageFile.CopyToAsync(stream);
                        dbItem.mpnComImage = $"/images/{filename}";
                    }

                    _context.Update(dbItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MpnComputer.Any(e => e.mpnComId == model.mpnComId))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(MpnIndex));
            }
            return View(model);
        }
        public async Task<IActionResult> MpnDelete(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.MpnComputer.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }
        [HttpPost, ActionName("HvtDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MpnDeleteConfirmed(int id)
        {
            var item = await _context.MpnComputer.FindAsync(id);
            if (item != null)
            {
                _context.MpnComputer.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MpnIndex));
        }
    }
}
