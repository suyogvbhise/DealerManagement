using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DMS.Models;
using DMS.Data; // Update based on your project namespace

public class ReportsController : Controller
{
    private readonly DMS.Data.ApplicationDbContext _context;

    public ReportsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Total PDNs Count
        var totalPDNs = await _context.PurchaseDeliveryNotes.CountAsync();

        // Total Inventory Count
        var totalInventory = await _context.Inventory.CountAsync();

        // Most Common Fuel Type
        var mostCommonFuelType = await _context.Inventory
            .GroupBy(i => i.FuelType.Name)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefaultAsync();

        // Top 5 Purchase Locations
        var topPurchaseLocations = await _context.Inventory
            .GroupBy(i => i.PurchaseLocations.Name)
            .OrderByDescending(g => g.Count())
            .Select(g => new { Location = g.Key, Count = g.Count() })
            .Take(5)
            .ToListAsync();

        // Latest 10 PDNs
        var latestPDNs = await _context.PurchaseDeliveryNotes
            .Include(p => p.Inventory)
            .ThenInclude(i => i.FuelType)
            .Include(p => p.Inventory.PurchaseLocations)
            .OrderByDescending(p => p.Id)
            .Take(10)
            .ToListAsync();

        // Pass data to View
        ViewBag.TotalPDNs = totalPDNs;
        ViewBag.TotalInventory = totalInventory;
        ViewBag.MostCommonFuelType = mostCommonFuelType ?? "N/A";
        ViewBag.TopPurchaseLocations = topPurchaseLocations;
        ViewBag.LatestPDNs = latestPDNs;

        return View();
    }
}
