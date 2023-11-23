using Application.DAOInterface;
using Domain.Model;

namespace InstantDataAccess.DAO;

public class RiskCategoryInstantDAO : IRiskCategoryDAO
{
    private readonly InstantDataContext context;

    public RiskCategoryInstantDAO(InstantDataContext context)
    {
        this.context = context;
    }
    public Task<RiskCategory> CreateAsync(RiskCategory category)
    {
        int categoryId = 1;
        if (context.RiskCategories.Any())
        {
            categoryId = context.RiskCategories.Max(c => c.CategoryId);
            categoryId++;
        }

        category.CategoryId = categoryId;
        context.RiskCategories.Add(category);
        context.SaveChanges();

        return Task.FromResult(category);
    }

    public async Task<RiskCategory> GetByIdAsync(int categoryId)
    {
        RiskCategory? existing = context.RiskCategories.FirstOrDefault(c =>
            c.CategoryId == categoryId);
        if(existing == null)
        {
            throw new Exception("Risk category not found");
        }

        return await Task.FromResult(existing);
    }

    public Task<List<RiskCategory>> GetAllAsync()
    {
        return Task.FromResult(context.RiskCategories.ToList());
    }
}