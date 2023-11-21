using Domain.DTO;
using Domain.Model;

namespace Application.LogicInterface;

public interface IRiskLogic
{
    void CreateFromFile(string type, int score, string description);
    Task<RiskCategory> CreateFromFile(string name);
    Task<List<RiskCategory>> GetRiskCategories(RiskCategoriesSearchParamDTO dto);
    Task<RiskCategory> GetById(int id);
}