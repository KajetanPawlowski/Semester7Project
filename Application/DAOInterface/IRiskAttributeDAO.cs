using Domain.Model;

namespace Application.DAOInterface;

public interface IRiskAttributeDAO
{
    Task<RiskAttribute> CreateAsync(RiskAttribute riskAttribute);
    Task<RiskAttribute?> GetByIdAsync(int attributeId);
    Task<List<RiskAttribute>> GetAllAsync();
}