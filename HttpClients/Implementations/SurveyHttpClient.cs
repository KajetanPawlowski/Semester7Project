using System.Text;
using System.Text.Json;
using Domain.DTO;
using Domain.Model;
using HttpClients.Interfaces;

namespace HttpClients.Implementations;

public class SurveyHttpClient : ISurveyHttpClient
{
    private readonly HttpClient client;
    private readonly IAuthHttpClient authHttpClient;
    
    public SurveyHttpClient(HttpClient client, IAuthHttpClient authHttpClient)
    {
        this.client = client;
        this.authHttpClient = authHttpClient;
    }
    
    public async Task<List<RiskCategory>> GetCategoriesAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/Risk/Category");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response +"");
        }

        List<RiskCategory> categories = JsonSerializer.Deserialize<List<RiskCategory>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return categories;
    }

    public Task<List<Risk>> GetGenericRisksAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> GetRiskAttributesTypesAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/Risk/Attribute");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response +"");
        }

        List<RiskAttribute> attributes = JsonSerializer.Deserialize<List<RiskAttribute>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        List<string> attributeTypes = attributes.Select(attr => attr.AttributeType).Distinct().ToList();
        return attributeTypes;
    }

    public async Task<List<RiskAttribute>> GetRiskAttributesAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/Risk/Attribute");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response +"");
        }

        List<RiskAttribute> attributes = JsonSerializer.Deserialize<List<RiskAttribute>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return attributes;
    }

    public async Task<Risk> AddRiskAsync(CreateRiskDTO dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(dtoAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync("/Risk", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        Risk created = JsonSerializer.Deserialize<Risk>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return created;
    }

    public Task<List<Question>> GetQuestionsAsync(int supplierId)
    {
        throw new NotImplementedException();
    }

    public async Task<Survey> CreateSurveyAsync(CreateSurveyDTO dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(dtoAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync("/Survey", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        Survey created = JsonSerializer.Deserialize<Survey>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return created;
    }

    public Task<Survey> AnswerSurveyAsync(int surveyId, Survey answeredSurvey)
    {
        throw new NotImplementedException();
    }
    
}