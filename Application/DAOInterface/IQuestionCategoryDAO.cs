using Domain.Model;

namespace Application.DAOInterface;

public interface IQuestionCategoryDAO
{
    Task<QuestionCategory> CreateAsync(QuestionCategory category);
    Task<QuestionCategory> GetByIdAsync(int categoryId);
    Task<List<QuestionCategory>> GetAllAsync();
}