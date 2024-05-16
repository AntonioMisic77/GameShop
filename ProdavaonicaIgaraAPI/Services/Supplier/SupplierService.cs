using AutoMapper;
using ProdavaonicaIgaraAPI.Data.Exceptions;
using ProdavaonicaIgaraAPI.Data.Supplier;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;

namespace ProdavaonicaIgaraAPI.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        public SupplierService(ISupplierRepository supplierRepository,IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SupplierDto>> GetSuppliersAsync()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }

        public async Task<SupplierDto> CreateSupplierAsync(SupplierDto supplierDto)
        {
            var checkEmail = await _supplierRepository.CheckSupplierUniqueEmail(supplierDto.Email);

            if (checkEmail)
            {
                throw new UniqueConstraint("Supplier with this email already exists");
            }

            var supplier = await _supplierRepository.CreateAsync(_mapper.Map<Supplier>(supplierDto));

            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task<SupplierDto> UpdateSupplierAsync(SupplierDto supplierDto)
        {
            var checkEmail = await _supplierRepository.CheckSupplierUniqueEmail(supplierDto.Email);

            if (checkEmail)
            {
                throw new UniqueConstraint("Supplier with this email already exists");
            }

            var supplier = await _supplierRepository.UpdateAsync(_mapper.Map<Supplier>(supplierDto));

            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task<SupplierDto> DeleteSupplierAsync(int id)
        {
            var supplier = await _supplierRepository.DeleteAsync(id);

            return _mapper.Map<SupplierDto>(supplier);
        }   
    }
}
