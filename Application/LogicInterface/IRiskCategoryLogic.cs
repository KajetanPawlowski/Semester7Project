using Domain.DTO;
using Domain.Model;

namespace Application.LogicInterface;

public interface IRiskCategoryLogic
{
    Task<RiskCategory> CreateFromFile(string name);
    Task<List<RiskCategory>> GetRiskCategories(RiskCategoriesSearchParamDTO dto);
    Task<RiskCategory> GetById(int id);
}
