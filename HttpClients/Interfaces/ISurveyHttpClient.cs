using Domain.DTO;
using Domain.Model;

namespace HttpClients.Interfaces;

public interface ISurveyHttpClient
{
    //Using Risk Logic
    Task<List<RiskCategory>> GetCategoriesAsync();
    Task<List<Risk>> GetGenericRisksAsync(int categoryId);
    Task<List<string>> GetRiskAttributesTypesAsync();
    Task<List<string>> GetRiskAttributesAsync(string attributeType);
    Task<Risk> QualifyRiskAsync(Risk risk, List<RiskAttribute> attributes);
    
    //Using Survey Logic
    Task<List<Question>> GetQuestionsAsync(int supplierId);
    Task<Survey> CreateSurveyAsync(CreateSurveyDTO dto);
    Task<Survey> AnswerSurveyAsync(int surveyId, Survey answeredSurvey);
    
    //Using Supplier Logic
    Task AssignCategoryAsync(int supplierId, int categoryId);
    Task<List<Risk>> AddSpecificRiskAsync(int supplierId, Risk specificRisk);
    
    //Using User Logic 
    
    //Using several
    
}