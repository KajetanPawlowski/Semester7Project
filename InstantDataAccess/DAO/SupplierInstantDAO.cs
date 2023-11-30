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
        context.SaveChanges();
        
        return Task.FromResult(supplier);
    }

    public async Task<Supplier> GetByIdAsync(int supplierId)
    {
        Supplier? existing = context.Suppliers.FirstOrDefault(s =>
            s.Id == supplierId);
        if(existing == null)
        {
            throw new Exception("Supplier not found");
        }

        return await Task.FromResult(existing);
    }

    public Task<List<Supplier>> GetAllAsync()
    {
        return Task.FromResult(context.Suppliers.ToList());
    }

    public Task<Supplier> UpdateAsync(Supplier supplier)
    {
        Supplier? existing = context.Suppliers.FirstOrDefault(s => s.Id== supplier.Id);
        if (existing == null)
        {
            throw new Exception("Supplier not found");
        }
        context.Suppliers.Remove(existing);
        context.Suppliers.Add(supplier);
        
        
        context.SaveChanges();
        return Task.FromResult(existing);
    }
}