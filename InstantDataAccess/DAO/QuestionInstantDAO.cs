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
        int questionId = 1;
        if (context.Questions.Any())
        {
            questionId = context.Questions.Max(q => q.Id);
            questionId++;
        }

        question.Id = questionId;
        context.Questions.Add(question);
        context.SaveChanges();

        return Task.FromResult(question);
    }

    public async Task<Question> GetByIdAsync(int questionId)
    {
        Question? existing = context.Questions.FirstOrDefault(q =>
            q.Id == questionId);
        if(existing == null)
        {
            throw new Exception("Question not found");
        }

        return await Task.FromResult(existing);
    }

    public Task<IEnumerable<Question>> GetByCategory(RiskCategory category)
    {
        IEnumerable<Question> questions = context.Questions
            .Where(q => q.Category.Equals(category))
            .ToList();

        return Task.FromResult(questions);
    }

    public Task<IEnumerable<Question>> GetByAttribute(RiskAttribute attribute)
    {
        IEnumerable<Question> questions = context.Questions
            .Where(q => q.RelatedRisk.RiskAttributes.Equals(attribute))
            .ToList();

        return Task.FromResult(questions);
    }

    public Task<List<Question>> GetAllAsync()
    {
        return Task.FromResult(context.Questions.ToList());
    }
}