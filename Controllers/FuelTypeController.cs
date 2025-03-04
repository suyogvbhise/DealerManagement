using DMS.Data;
using DMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.Controllers
{
    public class FuelTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FuelTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // List All Fuel Types
        public async Task<IActionResult> Index()
        {
            var fuelTypes = await _context.FuelTypes.ToListAsync();
            return View(fuelTypes);
        }

        // GET: Create Fuel Type
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create Fuel Type
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] FuelType fuelType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fuelType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to Fuel Type List
            }
            return View(fuelType);
        }
    }
}
