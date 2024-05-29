using MarianaMVCApp.Areas.Manage.ViewModels;
using MarianaMVCApp.DAL;
using MarianaMVCApp.Models;
using MarianaMVCApp.Utilites.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarianaMVCApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVm createVm)
        {
            if (!ModelState.IsValid)
            {
                return View(createVm);
            }
            if (!createVm.Photo.ValidateSize(5))
            {
                ModelState.AddModelError("Photo", "Your photo size is not true");
                return View(createVm);
            }
            if (!createVm.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "Your photo type is not true");
                return View(createVm);
            }
            
            Employee employee = new Employee
            {
                FullName = createVm.FullName,
                Position= createVm.Position,    
            };
            employee.Image = await createVm.Photo.CreateFile(_env.WebRootPath, "assets", "images", "timeline");
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id < 0) return BadRequest();
            Employee employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null) return NotFound();
            EmployeeUpdateVm updateVm = new EmployeeUpdateVm
            {
                FullName= employee.FullName,
                Position= employee.Position,    
                Image = employee.Image,

            };
            return View(updateVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,EmployeeUpdateVm updateVm)
        {
            if (id < 0) return BadRequest();
            Employee existed = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View(updateVm);
            }
            if (updateVm.Photo != null)
            {
                if (!updateVm.Photo.ValidateSize(5))
                {
                    ModelState.AddModelError("Photo", "Your photo size is not true");
                    return View(updateVm);
                }
                if (!updateVm.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "Your photo type is not true");
                    return View(updateVm);
                }
                string fileName = await updateVm.Photo.CreateFile(_env.WebRootPath, "assets", "images", "timeline");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "timeline");
                existed.Image = fileName;
            }
            existed.FullName = updateVm.FullName;
            existed.Position = updateVm.Position;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0) return BadRequest();
            Employee existed = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();
            existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "timeline");
            _context.Employees.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
