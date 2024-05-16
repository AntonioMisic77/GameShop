using Microsoft.AspNetCore.Mvc;
using ProdavaonicaIgaraAPI.Data.Supplier;
using ProdavaonicaIgaraAPI.Services;

namespace ProdavaonicaIgaraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<SupplierDto>> GetSuppliersAsync()
        {
            var supplier = await _supplierService.GetSuppliersAsync();

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }


        [HttpPost]
        public async Task<ActionResult<SupplierDto>> CreateSupplierAsync(SupplierDto supplierDto)
        {
            var supplier = await _supplierService.CreateSupplierAsync(supplierDto);

            if (supplier == null)
            {
                return BadRequest();
            }

            return Ok(supplier);
        }


        [HttpPut]
        public async Task<ActionResult<SupplierDto>> UpdateSupplierAsync(SupplierDto supplierDto)
        {
            var supplier = await _supplierService.UpdateSupplierAsync(supplierDto);

            if (supplier == null)
            {
                return BadRequest();
            }

            return Ok(supplier);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SupplierDto>> DeleteSupplierAsync(int id)
        {
            var supplier = await _supplierService.DeleteSupplierAsync(id);

            if (supplier == null)
            {
                return BadRequest();
            }

            return Ok(supplier);
        }

    }
}
