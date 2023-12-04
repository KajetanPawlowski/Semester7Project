using Domain.DTO;
using Domain.Model;

namespace Application.LogicInterface;

public interface IRiskLogic
{
    Task<Risk> CreateRisk(CreateRiskDTO dto);
    Task<RiskAttribute> CreateRiskAttributeFromFile(string type, int score, string description);
    Task<RiskCategory> CreateRiskCategoryFromFile(string name);
    Task<RiskCategory> GetRiskCategoryById(int categoryId);
    Task<List<Risk>> GetGenericRiskByCategory(int categoryId);
    Task<List<RiskAttribute>> GetRiskAttributesByType(string type);
    Task<List<RiskAttribute>> GetRiskAttributes();
    Task<List<RiskCategory>> GetRiskCategories();
    Task<Risk> QualifyRisk(Risk risk, List<RiskAttribute> attributes);
    Task<List<Risk>> GetRisksAsync();

}