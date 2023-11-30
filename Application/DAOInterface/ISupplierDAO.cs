using Domain.Model;

namespace Application.DAOInterface;

public interface ISupplierDAO
{
    Task<Supplier> CreateAsync(Supplier supplier);
    Task<Supplier> GetByIdAsync(int supplierId);
    Task<List<Supplier>> GetAllAsync();
    Task<Supplier> UpdateAsync(Supplier supplier);
    
}