using DMS.Data;
using DMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DMS.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inventory
        public async Task<IActionResult> Index()
        {
            var inventoryList = await _context.Inventory
                //.Include(i => i.RTOState)
                //.Include(i => i.RTOCity)
                .ToListAsync();
            return View(inventoryList);
        }

        // GET: Inventory/Create
        public IActionResult Create()
        {
            ViewBag.PurchaseLocations = _context.PurchaseLocations.ToList();
            ViewBag.FuelTypes = _context.FuelTypes.ToList(); // Fetch fuel types

            //ViewBag.RTOStates = _context.RTOStates.ToList();
            //ViewBag.RTOCities = _context.RTOCities.ToList();
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var inventory = await _context.Inventory
                .Include(i => i.FuelType) // Include FuelType details
                .Include(i => i.PurchaseLocations) // Include PurchaseLocation details
                .FirstOrDefaultAsync(m => m.Id == id);

            if (inventory == null)
                return NotFound();

            return View(inventory);
        }


        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationNumber,RegistrationDate,VehicleOwnership,PurchaseLocationId,FuelTypeId")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.PurchaseLocations = _context.PurchaseLocations.ToList();

            return View(inventory);
        }


        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
                return NotFound();


            return View(inventory);
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegistrationNumber,RegistrationDate,VehicleOwnership")] Inventory inventory)
        {
            if (id != inventory.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
                return NotFound();

            return View(inventory);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadWord(int id)
        {
            var inventory = await _context.Inventory
                .Include(i => i.FuelType)
                .Include(i => i.PurchaseLocations)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventory == null)
            {
                return NotFound();
            }

            using (MemoryStream stream = new MemoryStream())
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document, true))
                {
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = new Body();

                    body.Append(new Paragraph(new Run(new Text("Inventory Details")))
                    {
                        ParagraphProperties = new ParagraphProperties(new Justification { Val = JustificationValues.Center })
                    });

                    body.Append(new Paragraph(new Run(new Text($"Product ID: {inventory.Id}"))));
                    body.Append(new Paragraph(new Run(new Text($"Registration Number: {inventory.RegistrationNumber}"))));
                    body.Append(new Paragraph(new Run(new Text($"Fuel Type: {inventory.FuelType?.Name ?? "N/A"}"))));
                    body.Append(new Paragraph(new Run(new Text($"Purchase Location: {inventory.PurchaseLocations?.Name ?? "N/A"}"))));

                    mainPart.Document.Append(body);
                    mainPart.Document.Save();
                }

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"Inventory_{inventory.Id}.docx");
            }
        }


        public async Task<IActionResult> DownloadWord1(int id)
        {
            var inventory = await _context.Inventory
                .Include(i => i.FuelType)
                .Include(i => i.PurchaseLocations)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventory == null)
            {
                return NotFound();
            }

            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "InventoryTemplate.docx");
            string tempFilePath = Path.Combine(Path.GetTempPath(), $"InventoryTemplate{id}.docx");

            System.IO.File.Copy(templatePath, tempFilePath, true);

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(tempFilePath, true))
            {
                string docText = string.Empty;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                // Replace placeholders with actual values
                docText = docText.Replace("[PRODUCT_ID]", inventory.Id.ToString())
                                 .Replace("[REGISTRATION]", inventory.RegistrationNumber ?? "N/A")
                                 .Replace("[FUEL_TYPE]", inventory.FuelType?.Name ?? "N/A")
                                 .Replace("[PURCHASE_LOCATION]", inventory.PurchaseLocations?.Name ?? "N/A");

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(tempFilePath);
            System.IO.File.Delete(tempFilePath);

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"Inventory_{id}.docx");
        }
    
    }
}

