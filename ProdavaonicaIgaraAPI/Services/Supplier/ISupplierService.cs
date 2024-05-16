using ProdavaonicaIgaraAPI.Data.Supplier;

namespace ProdavaonicaIgaraAPI.Services
{
    public interface ISupplierService
    {

        public Task<IEnumerable<SupplierDto>> GetSuppliersAsync();

        public Task<SupplierDto> CreateSupplierAsync(SupplierDto supplierDto);

        public Task<SupplierDto> UpdateSupplierAsync(SupplierDto supplierDto);

        public Task<SupplierDto> DeleteSupplierAsync(int id);

    }
}
