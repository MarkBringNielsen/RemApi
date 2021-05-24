using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemApi.Models;
using RemApi.DTOs;

namespace RemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly RemaDbContext _context;

        public CategoriesController(RemaDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) { return NotFound(); }

            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryDTO category)
        {
            var newCategory = new Category(category);
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategories), newCategory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> PutCategory(Guid id, CategoryDTO category)
        {
            var newCategory = new Category(category, id);
            _context.Entry(newCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Categories.Any(s => s.ID == id)) { return NotFound(); }
                else { throw; }
            }

            return await _context.Categories.FindAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) { return NotFound(); }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}