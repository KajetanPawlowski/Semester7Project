using Domain.DTO;
using Domain.Model;

namespace Application.LogicInterface;

public interface ISupplierLogic
{
    Task<Supplier> AssignNewRiskCategory(int supplierId, int categoryId);
    Task<List<SpecificRisk>> AssignSpecificRisk(int supplierId, SpecificRisk specificRisk);
    Task<Supplier> GetSupplierByMail(string supplierMail);
    Task<List<Survey>> GetSurveysAsync(int supplierId);
    Task<Supplier> CreateSupplierAsync(SupplierCreationDTO dto);
    Task<List<Supplier>> GetSuppliersAsync();
}