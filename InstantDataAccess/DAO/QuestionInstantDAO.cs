using Application.DAOInterface;
using Domain.Model;

namespace InstantDataAccess.DAO;

public class QuestionInstantDAO : IQuestionDAO
{
    private readonly InstantDataContext context;

    public QuestionInstantDAO(InstantDataContext context)
    {
        this.context = context;
    }
    public Task<Question> CreateAsync(Question question)
    {
        throw new NotImplementedException();
    }

    public Task<Question> GetByIdAsync(int questionId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Question>> GetByCategory(RiskCategory category)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Question>> GetByAttribute(RiskAttribute attribute)
    {
        throw new NotImplementedException();
    }

    public Task<List<Question>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}