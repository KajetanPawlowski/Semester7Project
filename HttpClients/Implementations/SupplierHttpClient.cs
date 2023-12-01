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

    public async Task<List<Country>> GetAllCountries()
    {
        HttpResponseMessage response = await client.GetAsync("/Supplier/Country");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response +"");
        }

        List<Country> countries = JsonSerializer.Deserialize<List<Country>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return countries;
    }

    public async Task<List<Country>> GetCountriesByCCode(string cCode)
    {
        HttpResponseMessage response = await client.GetAsync("/Supplier/Country?cCode="+cCode);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response +"");
        }

        List<Country> countries = JsonSerializer.Deserialize<List<Country>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return countries;
    }

    public async Task<List<Country>> GetCountriesByRegion(string region)
    {
        HttpResponseMessage response = await client.GetAsync("/Supplier/Country?region="+region);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response +"");
        }

        List<Country> countries = JsonSerializer.Deserialize<List<Country>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return countries; 
    }

    public async Task<List<Survey>> GetSurveys(int supplierId)
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync("/Survey?supplierId=" +supplierId);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to retrieve surveys. Status code: {response.StatusCode}");
            }

            string result = await response.Content.ReadAsStringAsync();
            List<Survey> surveys = JsonSerializer.Deserialize<List<Survey>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        
            return surveys;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving surveys: {ex.Message}");
            throw;
        }
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

    public async Task<Supplier> GetSupplierByRepMail(string repEmail)
    {
        string serializedMail = JsonSerializer.Serialize(repEmail);
        HttpResponseMessage response = await client.GetAsync("/Supplier?repEmail="+serializedMail);
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

    public void NotifySupplier(int surveyId)
    {
        throw new NotImplementedException();
    }
    
}