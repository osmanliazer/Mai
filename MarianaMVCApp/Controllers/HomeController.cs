using MarianaMVCApp.DAL;
using MarianaMVCApp.Models;
using MarianaMVCApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MarianaMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVm homeVm = new HomeVm
            {
                Employees = await _context.Employees.ToListAsync(),
                Projects = await _context.Projects.ToListAsync(),
                Services = await _context.Services.ToListAsync(),
            };
            return View(homeVm);
        }
    }
}