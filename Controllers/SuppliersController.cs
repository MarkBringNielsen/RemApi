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
    public class SuppliersController : ControllerBase
    {
        private readonly RemaDbContext _context;

        public SuppliersController(RemaDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if(supplier == null) { return NotFound(); }

            return supplier;
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(SupplierDTO supplier)
        {
            var newSupplier = new Supplier(supplier);
            _context.Suppliers.Add(newSupplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSuppliers), newSupplier);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Supplier>> PutSupplier(Guid id, SupplierDTO supplier)
        {
            var newSupplier = new Supplier(supplier, id);

            _context.Entry(newSupplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Suppliers.Any(s => s.ID == id)) { return NotFound(); }
                else { throw; }
            }

            return await _context.Suppliers.FindAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) { return NotFound();}

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}