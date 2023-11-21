using Domain.DTO;
using Domain.Model;

namespace HttpClients.Interfaces;

public interface ISurveyHttpClient
{
    Task<List<RiskCategory>> GetCategoriesAsync();
    Task AssignCategoryAsync(int supplierId, int categoryId);
    Task<List<IRisk>> GetGenericRisksAsync(int categoryId);
    Task<List<string>> GetRiskAttributesTypesAsync();
    Task<List<string>> GetRiskAttributesAsync(string attributeType);
    Task AddSpecificRisksAsync(int supplierId, List<IRisk> specificRisks);
    Task<List<Question>> GetQuestionsAsync(int supplierId);
    Task<Survey> CreateSurveyAsync(CreateSurveyDTO dto);
    Task<Survey> AnswerSurveyAsync(int surveyId, Survey answeredSurvey);
}