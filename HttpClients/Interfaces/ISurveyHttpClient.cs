using Domain.DTO;
using Domain.Model;

namespace HttpClients.Interfaces;

public interface ISurveyHttpClient
{
    //Using Risk Logic
    Task<List<RiskCategory>> GetCategoriesAsync();
    Task<List<Risk>> GetGenericRisksAsync();
    Task<List<string>> GetRiskAttributesTypesAsync();
    Task<List<RiskAttribute>> GetRiskAttributesAsync();
    Task<Risk> AddRiskAsync(CreateRiskDTO dto);
    
    
    //Using Survey Logic
    Task<List<Question>> GetQuestionsAsync(int supplierId);
    Task<Survey> CreateSurveyAsync(CreateSurveyDTO dto);
    Task<Survey> GetSurveyByIdAsync(int surveyId);
    Task<Survey> AnswerSurveyAsync(AnswerSurveyDTO dto);
    
    //Using Supplier Logic
    
    //Using User Logic 
    
    //Using several
    
}