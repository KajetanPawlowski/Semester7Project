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
    public Task<List<Supplier>> GetSuppliers();
    
    //Using User Logic
    public Task<string> GetSupplierRepMail(int repId);
    
    //Using Several
    
    //Not Implemented
    public Task<Survey> ResendSurvey(int surveyId);
}