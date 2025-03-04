using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMS.Data;
using DMS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InventoryApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ 1. GET ALL INVENTORY ITEMS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventory()
        {
            return await _context.Inventory.ToListAsync();
        }

        // ✅ 2. GET INVENTORY ITEM BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventoryItem(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }

        // ✅ 3. CREATE NEW INVENTORY ITEM
        [HttpPost]
        public async Task<ActionResult<Inventory>> CreateInventory(Inventory inventory)
        {
            _context.Inventory.Add(inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInventoryItem), new { id = inventory.Id }, inventory);
        }

        // ✅ 4. UPDATE INVENTORY ITEM
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(int id, Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // ✅ 5. DELETE INVENTORY ITEM
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.Id == id);
        }
    }
}
