using Application.DAOInterface;
using Domain.Model;

namespace InstantDataAccess.DAO;

public class SupplierInstantDAO : ISupplierDAO
{
    private readonly InstantDataContext context;

    public SupplierInstantDAO(InstantDataContext context)
    {
        this.context = context;
    }
    public Task<Supplier> CreateAsync(Supplier supplier)
    {
        int nextId = 1;
        if (context.Suppliers.Any())
        {
            nextId = context.Suppliers.Max(s => s.Id);
            nextId++;
        }

        supplier.Id = nextId;
        
        context.Suppliers.Add(supplier);
        
        return Task.FromResult(supplier);
    }

    public Task<Supplier> GetByIdAsync(int supplierId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Supplier>> GetAllAsync()
    {
        return Task.FromResult(context.Suppliers.ToList());
    }

    public Task<Supplier> UpdateAsync(Supplier supplier)
    {
        throw new NotImplementedException();
    }
}