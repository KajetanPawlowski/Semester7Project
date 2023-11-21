using Domain.Model;

namespace Application.DAOInterface;

public interface IRiskAttributeDAO
{
    Task<RiskAttribute> CreateAsync(RiskAttribute riskAttribute);
}