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
    public class ProductsController : ControllerBase
    {
        private readonly RemaDbContext _context;
        private readonly CreateService<Product> _createService;
        private readonly ProductReadService _readService;
        private readonly UpdateService<Product> _updateService;
        private readonly DeleteService<Product> _deleteService;

        public ProductsController(RemaDbContext context)
        {
            _context = context;
            _createService = new CreateService<Product>(context.Products, context);
            _readService = new ProductReadService(context.Products);
            _updateService = new UpdateService<Product>(context.Products, context);
            _deleteService = new DeleteService<Product>(context.Products, context);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _readService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            return await _readService.GetAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDTO product)
        {          
            var supplier = await _context.Suppliers.FindAsync(product.SupplierID);
            if (supplier == null) { return BadRequest("Supplier does not exist"); }
        
            var category = await _context.Categories.FindAsync(product.CategoryID);
            if (category == null) { return BadRequest("Categhory does not exist"); }
            
            var newProduct = new Product(product, category, supplier);
            await _createService.Create(newProduct);

            return CreatedAtAction(nameof(GetProducts), newProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(Guid id, ProductDTO product)
        {        
            var supplier = await _context.Suppliers.FindAsync(product.SupplierID);
            if (supplier == null) { return BadRequest("Supplier does not exist"); }
        
            
            var category = await _context.Categories.FindAsync(product.CategoryID);
            if (category == null) { return BadRequest("Product does not exist"); }
            

            await _updateService.Update(new Product(product, category, supplier, id));

            return await _readService.GetAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _deleteService.DeleteAsync(id);

            return NoContent();
        }
    }

}