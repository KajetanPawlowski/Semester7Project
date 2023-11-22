using Application.DAOInterface;
using Domain.Model;

namespace InstantDataAccess.DAO;

public class RiskAttributeInstantDAO : IRiskAttributeDAO
{
    private readonly InstantDataContext context;

    public RiskAttributeInstantDAO(InstantDataContext context)
    {
        this.context = context;
    }
    public Task<RiskAttribute> CreateAsync(RiskAttribute riskAttribute)
    {
        throw new NotImplementedException();
    }

    public Task<RiskAttribute> GetByIdAsync(int attributeId)
    {
        throw new NotImplementedException();
    }

    public Task<List<RiskAttribute>> GetByTypeAsync(string type)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetAttributeTypesAsync()
    {
        throw new NotImplementedException();
    }
}