using Domain.DTO;
using Domain.Model;

namespace Application.LogicInterface;

public interface ISupplierLogic
{
    Task<Supplier> AssignNewRiskCategory(int supplierId, int categoryId);
    Task<Supplier> AssignSpecificRisk(int supplierId, Risk specificRisk);
    Task<Supplier> GetSupplierByMail(string supplierMail);
    Task<Supplier> GetSupplierById(int supplierId);
    Task<List<Survey>> GetSurveysAsync(int supplierId);
    Task<Supplier> CreateSupplierAsync(SupplierCreationDTO dto);
    Task<List<Supplier>> GetSuppliersAsync();
    Task<Country> GetCountryByCCode(string cCode);
    Task<List<Country>> GetCountriesByRegion(string countryRegion);
    Task<List<Country>> GetAllCountries();

}