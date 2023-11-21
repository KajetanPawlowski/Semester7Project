using Domain.DTO;
using Domain.Model;

namespace Application.LogicInterface;

public interface IRiskLogic
{
    Task<RiskAttribute> CreateRiskAttributeFromFile(string type, int score, string description);
    Task<RiskCategory> CreateRiskCategoryFromFile(string name);
    Task<RiskCategory> GetRiskCategoryById(int? categoryId);
    Task<List<IRisk>> GetGenericRiskByCategory(int categoryId);
    Task<List<RiskAttribute>> GetRiskAttributes();
    Task<List<RiskCategory>> GetRiskCategories(string categoryNameContent);
    Task<SpecificRisk> QualifyRisk(IRisk risk, List<RiskAttribute> attributes);

}