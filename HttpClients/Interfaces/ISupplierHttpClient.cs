using Domain.DTO;
using Domain.Model;

namespace HttpClients.Interfaces;

public interface ISupplierHttpClient
{
    public Task<Supplier> GetSupplierByEmail(string email);
    public Task<List<Survey>> GetAllSurveys(int supplierId);
    public Task<Survey> ResendSurvey(int surveyId);
    public Task<Supplier> CreateSupplier(SupplierCreationDTO dto);
    public Task<List<Supplier>> GetSuppliers();
}