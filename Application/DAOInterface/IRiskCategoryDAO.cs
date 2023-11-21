using Domain.DTO;
using Domain.Model;

namespace Application.DAOInterface;

public interface IRiskCategoryDAO
{
    Task<RiskCategory> CreateAsync(string categoryName);
    Task<List<RiskCategory>> GetCategoriesAsync(RiskCategoriesSearchParamDTO searchParamDTO);
    public Task<RiskCategory?> GetCategoryById(int id);
}