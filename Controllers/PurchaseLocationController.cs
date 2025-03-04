using DMS.Data;
using DMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class PurchaseLocationController : Controller
{
    private readonly ApplicationDbContext _context;

    public PurchaseLocationController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var locations = await _context.PurchaseLocations.ToListAsync();
        return View(locations);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name")] PurchaseLocation location)
    {
        if (ModelState.IsValid)
        {
            _context.PurchaseLocations.Add(location);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "Inventory"); // Redirect to Add New Car page
        }
        return View(location);
    }
}
