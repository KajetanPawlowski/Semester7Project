using Domain.Model;

namespace Application.DAOInterface;

public interface IQuestionDAO
{
    Task<Question> CreateAsync(Question question);
    Task<Question?> GetByIdAsync(int questionId);
    Task<List<Question>> GetAllAsync();
}