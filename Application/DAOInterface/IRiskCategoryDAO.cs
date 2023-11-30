using Domain.DTO;
using Domain.Model;

namespace Application.DAOInterface;

public interface IRiskCategoryDAO
{
    Task<RiskCategory> CreateAsync(RiskCategory category);
    Task<RiskCategory> GetByIdAsync(int categoryId);
    Task<List<RiskCategory>> GetAllAsync();
}