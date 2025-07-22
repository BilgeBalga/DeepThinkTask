using Microsoft.AspNetCore.Mvc;
using DeepThinkTask.Models;
using Microsoft.EntityFrameworkCore; 

namespace DeepThinkTask.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context; // Veritabanı erişimi için AppDbContext

        // Constructor
        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Yönetim Paneli Ana Sayfa";
            return View();
        }

        // GET: Admin/AboutUs
        public async Task<IActionResult> AboutUs()
        {
            ViewData["Title"] = "Hakkımızda Yönetimi";
            var aboutUs = await _context.AboutUs.FirstOrDefaultAsync();

            if (aboutUs == null)
            {
                aboutUs = new AboutUs
                {
                    Id = 1,
                    Content = "Lütfen hakkımızda içeriğini buradan güncelleyin.",
                    EstablishmentYear = "XXXX",
                    Mission = "Misyonunuzu girin.",
                    Vision = "Vizyonunuzu girin.",
                    ManagerName = "Yönetici Adı",
                    ManagerImagePath = ""
                };
                _context.AboutUs.Add(aboutUs);
                await _context.SaveChangesAsync();
            }

            return View(aboutUs);
        }

        // POST: Admin/AboutUs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AboutUs([Bind("Id,Content,EstablishmentYear,Mission,Vision,ManagerName,ManagerImagePath")] AboutUs aboutUs, IFormFile? managerImageFile)
        {
            if (aboutUs.Id != 1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Görsel yükleme işlemi
                if (managerImageFile != null && managerImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Eski görseli sil
                    if (!string.IsNullOrEmpty(aboutUs.ManagerImagePath))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", aboutUs.ManagerImagePath.TrimStart('~', '/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(managerImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await managerImageFile.CopyToAsync(fileStream);
                    }
                    aboutUs.ManagerImagePath = "~/uploads/" + uniqueFileName; 
                }
                else 
                {
                   
                    var existingAboutUs = await _context.AboutUs.AsNoTracking().FirstOrDefaultAsync(a => a.Id == 1);
                    if (existingAboutUs != null)
                    {
                        aboutUs.ManagerImagePath = existingAboutUs.ManagerImagePath;
                    }
                }

                try
                {
                    _context.Update(aboutUs);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Hakkımızda içeriği başarıyla güncellendi!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.AboutUs.Any(e => e.Id == aboutUs.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AboutUs));
            }
            ViewData["Title"] = "Hakkımızda Yönetimi";
            return View(aboutUs);
        }

        // GET: Admin/Sliders
        public IActionResult Sliders()
        {
            ViewData["Title"] = "Slider Yönetimi";
            var sliders = _context.Sliders.OrderBy(s => s.Order).ToList();
            return View(sliders); 
        }

        // GET: Admin/SliderCreate
        public IActionResult SliderCreate()
        {
            ViewData["Title"] = "Yeni Slider Ekle";
            return View(); // Boş bir form gönderecek
        }

        // POST: Admin/SliderCreate
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> SliderCreate(Slider slider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(slider); 
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Sliders)); 
            }
            ViewData["Title"] = "Yeni Slider Ekle";
            return View(slider); 
        }

        // GET: Admin/SliderEdit/
        public async Task<IActionResult> SliderEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound(); 
            }
            ViewData["Title"] = "Slider Düzenle";
            return View(slider); 
        }

        // POST: Admin/SliderEdit/
        // Slider düzenleme formundan gelen veriyi işleme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SliderEdit(int id, Slider slider)
        {
            if (id != slider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slider); 
                    await _context.SaveChangesAsync(); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Sliders.Any(e => e.Id == slider.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Sliders)); 
            }
            ViewData["Title"] = "Slider Düzenle";
            return View(slider);
        }

        // GET: Admin/SliderDelete/
        public async Task<IActionResult> SliderDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Slider Sil";
            return View(slider); 
        }

        // POST: Admin/SliderDelete/ (Onaylandıktan sonra silme işlemi)
        [HttpPost, ActionName("SliderDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider != null)
            {
                _context.Sliders.Remove(slider);
            }

            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Sliders));
        }
    }
}