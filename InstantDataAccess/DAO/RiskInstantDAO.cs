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
        throw new NotImplementedException();
    }

    public Task<Risk> GetByIdAsync(int riskId)
    {
        throw new NotImplementedException();
    }

    public Task<Risk> GetByRiskNameAsync(string riskNameContent)
    {
        throw new NotImplementedException();
    }

    public Task<List<Risk>> GetByCategoryIdAsync(int riskCategoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Risk>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Risk> UpdateRisk(Risk specificRisk)
    {
        throw new NotImplementedException();
    }
}