using Domain.DTO;
using Domain.Model;

namespace HttpClients.Interfaces;

public interface ISupplierHttpClient
{
    //Using Risk Logic
    
    //Using Survey Logic
    
    //Using Supplier Logic
    public Task<Supplier> GetSupplierById(int supplierId);
    public Task<List<Survey>> GetSurveys(int supplierId);
    public Task<Supplier> CreateSupplier(SupplierCreationDTO dto);
    public Task<Supplier> AddSupplierCategories(UpdateSupplierDTO dto);
    public Task<Supplier> UpdateSupplierRisks(UpdateSupplierDTO dto);
    public Task<List<Supplier>> GetSuppliers();
    public Task<Supplier> GetSupplierByRepMail(string repEmail);


    //Using User Logic

    //Using Several
    
    //Not Implemented
    public void NotifySupplier (int surveyId);
}