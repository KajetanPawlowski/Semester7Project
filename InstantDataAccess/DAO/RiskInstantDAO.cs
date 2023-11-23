using Application.DAOInterface;
using Domain.Model;

namespace InstantDataAccess.DAO;

public class RiskInstantDAO : IRiskDAO
{
    private readonly InstantDataContext context;
    
    public RiskInstantDAO(InstantDataContext context)
    {
        this.context = context;
    }
    public Task<Risk> CreateAsync(Risk risk)
    {
        int riskId = 1;
        if (context.Risks.Any())
        {
            riskId = context.Risks.Max(r => r.Id);
            riskId++;
        }

        risk.Id = riskId;
        context.Risks.Add(risk);
        context.SaveChanges();

        return Task.FromResult(risk);
    }

    public async Task<Risk> GetByIdAsync(int riskId)
    {
        Risk? existing = context.Risks.FirstOrDefault(r =>
            r.Id == riskId);
        if(existing == null)
        {
            throw new Exception("Risk not found");
        }

        return await Task.FromResult(existing);
    }

    public Task<Risk> GetByRiskNameAsync(string riskNameContent)
    {
        Risk? existing = context.Risks.FirstOrDefault(r => r.Name.Equals(riskNameContent));
        if (existing == null)
        {
            throw new Exception("Risk not found");
        }
        return Task.FromResult(existing);
        
    }

    public Task<List<Risk>> GetByCategoryIdAsync(int riskCategoryId)
    {
        List<Risk> risks = context.Risks
            .Where(r => r.Category.CategoryId.Equals(riskCategoryId))
            .ToList();

        return Task.FromResult(risks);
    }

    public Task<List<Risk>> GetAllAsync()
    {
        return Task.FromResult(context.Risks.ToList());
    }

    public Task<Risk> UpdateRisk(Risk specificRisk)
    {
        Risk? existing = context.Risks.FirstOrDefault(r=> r.Id== specificRisk.Id);
        if (existing == null)
        {
            throw new Exception("Risk not found");
        }
        context.Risks.Remove(existing);
        context.Risks.Add(specificRisk);
        
        
        context.SaveChanges();
        return (Task<Risk>)Task.CompletedTask;
    }
}