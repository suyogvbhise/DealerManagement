using DMS.Data;
using DMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DMS.Controllers
{
    public class PurchaseDeliveryNoteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseDeliveryNoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Create PDN
        public async Task<IActionResult> Create(int inventoryId)
        {
            var inventory = await _context.Inventory
                .Include(i => i.FuelType)
                .Include(i => i.PurchaseLocations)
                .FirstOrDefaultAsync(i => i.Id == inventoryId);

            if (inventory == null)
                return NotFound();

            var pdn = new PurchaseDeliveryNote { InventoryId = inventory.Id };
            ViewBag.Inventory = inventory; // Pass inventory details to the view

            return View(pdn);
        }

        // POST: Create PDN
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseDeliveryNote pdn)
        {
            if (ModelState.IsValid)
            {
                _context.PurchaseDeliveryNotes.Add(pdn);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Inventory");
            }

            ViewBag.Inventory = await _context.Inventory
                .Include(i => i.FuelType)
                .Include(i => i.PurchaseLocations)
                .FirstOrDefaultAsync(i => i.Id == pdn.InventoryId);

            return View(pdn);
        }
        public async Task<IActionResult> Index(string searchBuyer, string searchContact, string searchRegNumber)
        {
            var pdns = _context.PurchaseDeliveryNotes
    .Include(p => p.Inventory)
        .ThenInclude(i => i.FuelType) // Include Fuel Type
    .Include(p => p.Inventory)
        .ThenInclude(i => i.PurchaseLocations) // Include Purchase Location
    .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(searchBuyer))
            {
                pdns = pdns.Where(p => p.BuyerName.Contains(searchBuyer));
            }
            if (!string.IsNullOrEmpty(searchContact))
            {
                pdns = pdns.Where(p => p.ContactNumber.Contains(searchContact));
            }
            if (!string.IsNullOrEmpty(searchRegNumber))
            {
                pdns = pdns.Where(p => p.Inventory.RegistrationNumber.Contains(searchRegNumber));
            }

            return View(await pdns.ToListAsync());
        }

    }
}
