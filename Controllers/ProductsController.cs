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
    public class ProductsController : ControllerBase
    {
        private readonly RemaDbContext _context;

        public ProductsController(RemaDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.Include(p => p.Category).Include(p => p.Supplier).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await GetByID(id);
            if(product == null) { return NotFound(); }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDTO product)
        {          
            var supplier = await _context.Suppliers.FindAsync(product.SupplierID);
            if (supplier == null) { return BadRequest("Supplier does not exist"); }
        
            
            var category = await _context.Categories.FindAsync(product.CategoryID);
            if (category == null) { return BadRequest("Category does not exist"); }
            
            var newProduct = new Product(product, category, supplier);
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducts), newProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(Guid id, ProductDTO product)
        {        
            var supplier = await _context.Suppliers.FindAsync(product.SupplierID);
            if (supplier == null) { return BadRequest("Supplier does not exist"); }
        
            
            var category = await _context.Categories.FindAsync(product.CategoryID);
            if (category == null) { return BadRequest("Category does not exist"); }
            

            _context.Entry(new Product(product, category, supplier, id)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(s => s.ID == id)) { return NotFound(); }
                else { throw; }
            }

            return await GetByID(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.Include(p => p.Supplier).Include(p => p.Category).FirstOrDefaultAsync(p => p.ID == id);
            if (product == null) { return NotFound();}

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    
        private async Task<ActionResult<Product>> GetByID(Guid id){
            return await _context.Products.Include(p => p.Supplier).Include(p => p.Category).FirstOrDefaultAsync(p => p.ID == id);
        }
    }

}