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
    public class SuppliersController : ControllerBase
    {
        private readonly RemaDbContext _context;
        private readonly CreateService<Supplier> _createService;
        private readonly ReadService<Supplier> _readService;
        private readonly UpdateService<Supplier> _updateService;
        private readonly DeleteService<Supplier> _deleteService;

        

        public SuppliersController(RemaDbContext context)
        {
            _context = context;
            _createService = new CreateService<Supplier>(context.Suppliers, context);
            _readService = new ReadService<Supplier>(context.Suppliers);
            _updateService = new UpdateService<Supplier>(context.Suppliers, context);
            _deleteService = new DeleteService<Supplier>(context.Suppliers, context);
            
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return Ok(await _readService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(Guid id)
        {
            return await _readService.GetAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(SupplierDTO supplier)
        {
            var newSupplier = new Supplier(supplier);
            await _createService.Create(newSupplier);

            return CreatedAtAction(nameof(GetSuppliers), newSupplier);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Supplier>> PutSupplier(Guid id, SupplierDTO supplier)
        {
            var newSupplier = new Supplier(supplier, id);

            await _updateService.Update(newSupplier);

            return await _readService.GetAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {

            try
            {
                await _deleteService.DeleteAsync(id);
            }
            catch
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}