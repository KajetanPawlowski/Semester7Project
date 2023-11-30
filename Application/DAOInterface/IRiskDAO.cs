using Domain.Model;

namespace Application.DAOInterface;

public interface IRiskDAO
{
    Task<Risk> CreateAsync(Risk risk);
    Task<Risk> GetByIdAsync(int riskId);
    Task<Risk> GetByRiskNameAsync(string riskNameContent);
    Task<List<Risk>> GetByCategoryIdAsync(int riskCategoryId);
    Task<List<Risk>> GetAllAsync();
    Task<Risk> UpdateRisk(Risk specificRisk);
}