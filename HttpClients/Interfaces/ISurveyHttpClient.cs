using Domain.DTO;
using Domain.Model;

namespace HttpClients.Interfaces;

public interface ISurveyHttpClient
{
    //Using Risk Logic
    Task<List<RiskCategory>> GetCategoriesAsync();
    Task<List<Risk>> GetGenericRisksAsync(int categoryId);
    Task<List<string>> GetRiskAttributesTypesAsync();
    Task<List<RiskAttribute>> GetRiskAttributesAsync();
    Task<Risk> AddRiskAsync(CreateRiskDTO dto);
    
    
    //Using Survey Logic
    Task<List<Question>> GetQuestionsAsync(int supplierId);
    Task<Survey> CreateSurveyAsync(CreateSurveyDTO dto);
    Task<Survey> AnswerSurveyAsync(int surveyId, Survey answeredSurvey);
    
    //Using Supplier Logic
    
    //Using User Logic 
    
    //Using several
    
}