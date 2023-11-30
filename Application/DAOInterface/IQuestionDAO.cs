using Domain.Model;

namespace Application.DAOInterface;

public interface IQuestionDAO
{
    Task<Question> CreateAsync(Question question);
    Task<Question> GetByIdAsync(int questionId);
    Task<IEnumerable<Question>> GetByCategory(RiskCategory category);
    Task<IEnumerable<Question>> GetByAttribute(RiskAttribute attribute);
    Task<List<Question>> GetAllAsync();
}