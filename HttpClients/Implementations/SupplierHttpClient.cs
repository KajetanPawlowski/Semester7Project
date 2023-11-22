using System.Text.Json;
using Domain.DTO;
using Domain.Model;
using HttpClients.Interfaces;

namespace HttpClients.Implementations;

public class SupplierHttpClient : ISupplierHttpClient
{
    private readonly HttpClient client;
    private readonly IAuthHttpClient authHttpClient;

    public SupplierHttpClient(HttpClient client,  IAuthHttpClient authHttpClient)
    {
        this.client = client;
        this.authHttpClient = authHttpClient;
    }
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

    public async Task<List<Supplier>> GetSuppliers()
    {
        HttpResponseMessage response = await client.GetAsync("/Supplier");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response +"");
        }

        List<Supplier> suppliers = JsonSerializer.Deserialize<List<Supplier>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return suppliers;
    }

    public Task<Survey> ResendSurvey(int surveyId)
    {
        throw new NotImplementedException();
    }
}