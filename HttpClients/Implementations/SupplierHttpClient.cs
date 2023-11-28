using System.Text;
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
    public async Task<Supplier> GetSupplierById(int supplierId)
    {
        HttpResponseMessage response = await client.GetAsync("/Supplier?supplierId="+supplierId);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response +"");
        }

        List<Supplier> suppliers = JsonSerializer.Deserialize<List<Supplier>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return suppliers.First();
    }

    public Task<List<Survey>> GetSurveys(int supplierId)
    {
        throw new NotImplementedException();
    }

    public async Task<Supplier> CreateSupplier(SupplierCreationDTO dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(dtoAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync("/Supplier", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        Supplier created = JsonSerializer.Deserialize<Supplier>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return created;
    }

    public async Task<Supplier> AddSupplierCategories(UpdateSupplierDTO dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(dtoAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PatchAsync("/Supplier", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        Supplier updated = JsonSerializer.Deserialize<Supplier>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return updated;
    }

    public async Task<Supplier> UpdateSupplierRisks(UpdateSupplierDTO dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(dtoAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PatchAsync("/Supplier", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        Supplier updated = JsonSerializer.Deserialize<Supplier>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return updated;
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

    public Task<string> GetSupplierRepMail(int repId)
    {
        throw new NotImplementedException();
    }

    public Task<Survey> ResendSurvey(int surveyId)
    {
        throw new NotImplementedException();
    }
}