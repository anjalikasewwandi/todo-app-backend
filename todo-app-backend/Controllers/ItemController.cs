using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using todo_app_backend.Data;
using todo_app_backend.Models;

namespace todo_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ItemController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Item>> AddItem(Item newItem)
        {
            if (newItem != null)
            {
                _appDbContext.Items.Add(newItem);
                await _appDbContext.SaveChangesAsync();
                return Ok(newItem);
            }
            return BadRequest("Object instance not set");
        }

        // Read All Items
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllItems()
        {
            var items = await _appDbContext.Items.ToListAsync();
            return Ok(items);
        }

        // Read single item
        [HttpGet("{title}")]
        public async Task<ActionResult<Item>> GetItem(string title)
        {
            var item = await _appDbContext.Items.FirstOrDefaultAsync(e => e.Title == title);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound("Item is not available");
        }

        // Update item
        [HttpPut("{oldTitle}")]
        public async Task<ActionResult<Item>> UpdateItem(string oldTitle, Item updatedItem)
        {
            var existingItem = await _appDbContext.Items.FirstOrDefaultAsync(e => e.Title == oldTitle);
            if (existingItem == null)
            {
                return NotFound("Item not found");
            }

            // Check if the title has changed
            if (oldTitle != updatedItem.Title)
            {
                // Remove the old item and add the updated item as new
                _appDbContext.Items.Remove(existingItem);
                _appDbContext.Items.Add(updatedItem);
            }
            else
            {
                // If the title hasn't changed, just update the other fields
                existingItem.Description = updatedItem.Description;
                existingItem.Status = updatedItem.Status;
            }

            await _appDbContext.SaveChangesAsync();
            return Ok(updatedItem);
        }

        // Delete item
        [HttpDelete("{title}")]
        public async Task<ActionResult> DeleteItem(string title)
        {
            var item = await _appDbContext.Items.FindAsync(title);
            if (item != null)
            {
                _appDbContext.Items.Remove(item);
                await _appDbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound("Item not found");
        }
    }
}
