using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;

namespace Application.Logic;

public class SupplierLogic : ISupplierLogic
{
    public Task<Supplier> AssignNewRiskCategory(int supplierId, int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<SpecificRisk>> AssignSpecificRisk(int supplierId, SpecificRisk specificRisk)
    {
        throw new NotImplementedException();
    }

    public Task<Supplier> GetSupplierByMail(string supplierMail)
    {
        throw new NotImplementedException();
    }

    public Task<List<Survey>> GetSurveysAsync(int supplierId)
    {
        throw new NotImplementedException();
    }

    public Task<Supplier> CreateSupplierAsync(SupplierCreationDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<List<Supplier>> GetSuppliersAsync()
    {
        throw new NotImplementedException();
    }
}