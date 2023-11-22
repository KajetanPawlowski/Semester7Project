using Domain.DTO;
using Domain.Model;
using HttpClients.Interfaces;

namespace HttpClients.Implementations;

public class SupplierHttpClient : ISupplierHttpClient
{
    public Task<Supplier> GetSupplierByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<List<Survey>> GetSurveys(int supplierId)
    {
        throw new NotImplementedException();
    }

    public Task<Supplier> CreateSupplier(SupplierCreationDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<List<Supplier>> GetSuppliers()
    {
        throw new NotImplementedException();
    }

    public Task<Survey> ResendSurvey(int surveyId)
    {
        throw new NotImplementedException();
    }
}