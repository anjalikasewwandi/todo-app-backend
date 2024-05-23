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
        public async Task<ActionResult<List<Item>>> AddItem(Item newItem)
        {
            if (newItem != null)
            {
                _appDbContext.Items.Add(newItem);
                await _appDbContext.SaveChangesAsync();
                return Ok(await _appDbContext.Items.ToListAsync());
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
        [HttpPut("{title}")]
        public async Task<ActionResult<Item>> UpdateItem(string title, Item updatedItem)
        {
            if (updatedItem != null && title == updatedItem.Title)
            {
                _appDbContext.Entry(updatedItem).State = EntityState.Modified;
                await _appDbContext.SaveChangesAsync();
                return Ok(updatedItem);
            }
            return BadRequest("User not found");
        }

        // Delete item
        [HttpDelete("{title}")]
        public async Task<ActionResult> DeleteItem(string title)
        {
            var user = await _appDbContext.Items.FindAsync(title);
            if (user != null)
            {
                _appDbContext.Items.Remove(user);
                await _appDbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound("User not found");
        }
    }
}
