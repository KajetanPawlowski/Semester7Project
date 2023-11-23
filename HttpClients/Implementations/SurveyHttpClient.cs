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
    
    public Task<List<RiskCategory>> GetCategoriesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Risk>> GetGenericRisksAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetRiskAttributesTypesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<RiskAttribute>> GetRiskAttributesAsync(string attributeType)
    {
        throw new NotImplementedException();
    }

    public Task<Risk> QualifyRiskAsync(Risk risk, List<RiskAttribute> attributes)
    {
        throw new NotImplementedException();
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

    public Task AssignCategoryAsync(int supplierId, int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Risk>> AddSpecificRiskAsync(int supplierId, Risk specificRisk)
    {
        throw new NotImplementedException();
    }
}