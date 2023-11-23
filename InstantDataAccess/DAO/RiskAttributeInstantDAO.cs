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
        int riskAttributeId = 1;
        if (context.RiskAttributes.Any())
        {
            riskAttributeId = context.RiskAttributes.Max(r => r.AttributeId);
            riskAttributeId++;
        }

        riskAttribute.AttributeId = riskAttributeId;
        context.RiskAttributes.Add(riskAttribute);
        context.SaveChanges();

        return Task.FromResult(riskAttribute);
    }

    public async Task<RiskAttribute> GetByIdAsync(int attributeId)
    {
        RiskAttribute? existing = context.RiskAttributes.FirstOrDefault(r =>
            r.AttributeId == attributeId);
        if(existing == null)
        {
            throw new Exception("Risk attribute not found");
        }

        return await Task.FromResult(existing);
    }

    public Task<List<RiskAttribute>> GetByTypeAsync(string type)
    {
        List<RiskAttribute> riskAttributes = context.RiskAttributes
            .Where(r => r.AttributeType.Equals(type))
            .ToList();

        return Task.FromResult(riskAttributes);
    }

    public Task<List<string>> GetAttributeTypesAsync()
    {
        List<string> attributeTypes = context.RiskAttributes
            .Select(r => r.AttributeType).Distinct().ToList();

        return Task.FromResult(attributeTypes);
    }
}