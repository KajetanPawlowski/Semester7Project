using Domain.Model;

namespace Application.DAOInterface;

public interface ISurveyDAO
{
    Task<Survey> CreateAsync(Survey survey);
    Task<Survey?> GetByIdAsync(int surveyId);
    
    Task<List<Survey>> GetAllAsync();
}