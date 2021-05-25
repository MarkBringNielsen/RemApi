using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemApi.Models;
using RemApi.DTOs;
using RemApi.Data;
using RemApi.Services;

namespace RemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly RemaDbContext _context;
        private readonly CreateService<Category> _createService;
        private readonly ReadService<Category> _readService;
        private readonly UpdateService<Category> _updateService;
        private readonly DeleteService<Category> _deleteService;

        public CategoriesController(RemaDbContext context)
        {
            _context = context;
            _createService = new CreateService<Category>(context.Categories, context);
            _readService = new ReadService<Category>(context.Categories);
            _updateService = new UpdateService<Category>(context.Categories, context);
            _deleteService = new DeleteService<Category>(context.Categories, context);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _readService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await _readService.GetAsync(id);
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