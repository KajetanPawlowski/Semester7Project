using Application.DAOInterface;
using Domain.Model;

namespace InstantDataAccess.DAO;

public class QuestionCategoryInstantDAO : IQuestionCategoryDAO
{
    private readonly InstantDataContext context;

    public QuestionCategoryInstantDAO(InstantDataContext context)
    {
        this.context = context;
    }
    public Task<QuestionCategory> CreateAsync(QuestionCategory category)
    {
        IEnumerable<QuestionCategory> existing  = context.QuestionCategories.Where(cat => cat.RiskMappingId == category.RiskMappingId);
        existing = existing.Where(cat => cat.ImpactedGroupId == category.ImpactedGroupId);
        
        if (existing.Any())
        {
            return Task.FromResult(existing.ToList().First());
        }
        int categoryId = 1;
        if (context.QuestionCategories.Any())
        {
            categoryId = context.QuestionCategories.Max(cat => cat.Id);
            categoryId++;
        }
        category.Id = categoryId;
        category.Name = "Extraordinary";
        context.QuestionCategories.Add(category);
        context.SaveChanges();
        

        return Task.FromResult(category);
    }

    public Task<QuestionCategory> GetByIdAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<QuestionCategory>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}