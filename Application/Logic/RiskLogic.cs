using System.Runtime.InteropServices.ComTypes;
using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;


namespace Application.Logic;

public class RiskLogic : IRiskLogic
{
    private readonly IRiskAttributeDAO _riskAttributeDao;
    private readonly IRiskCategoryDAO _riskCategoryDao;
    private readonly IRiskDAO _riskDao;

    public RiskLogic(IRiskAttributeDAO riskAttributeDao, IRiskCategoryDAO riskCategoryDao, IRiskDAO riskDao)
    {
        _riskAttributeDao = riskAttributeDao;
        _riskCategoryDao = riskCategoryDao;
        _riskDao = riskDao;
    }
    
    public async Task<Risk> CreateRisk(string riskName, int riskCategoryId)
    {
        Risk? exist = await _riskDao.GetByRiskNameAsync(riskName);
        if (exist != null)
        {
            throw new Exception("Risk Already Exists!");
        }
        Risk genericRisk = new Risk();
        genericRisk.Name = riskName;
        genericRisk.Category = await GetRiskCategoryById(riskCategoryId);
        return await _riskDao.CreateAsync(genericRisk);

    }

    public Task<RiskAttribute> CreateRiskAttributeFromFile(string type, int score, string description)
    {
        throw new NotImplementedException();
    }

    public Task<RiskCategory> CreateRiskCategoryFromFile(string name)
    {
        throw new NotImplementedException();
    }

    public Task<RiskCategory> GetRiskCategoryById(int categoryId)
    {
        return _riskCategoryDao.GetByIdAsync(categoryId);
    }

    public Task<List<Risk>> GetGenericRiskByCategory(int categoryId)
    {
        return _riskDao.GetByCategoryIdAsync(categoryId);
    }

    public Task<List<RiskAttribute>> GetRiskAttributesByType(string type)
    {
        return _riskAttributeDao.GetByTypeAsync(type);
    }

    public Task<List<string>> GetRiskAttributesTypes()
    {
        return _riskAttributeDao.GetAttributeTypesAsync();
    }

    public Task<List<RiskCategory>> GetRiskCategories()
    {
        return _riskCategoryDao.GetAllAsync();
    }

    public async Task<Risk> QualifyRisk(Risk risk, List<RiskAttribute> attributes)
    {
        await _riskDao.GetByIdAsync(risk.Id);
        await ValidateRiskAttributes(attributes);
        risk.RiskAttributes = attributes;
        return await _riskDao.UpdateRisk(risk);
    }

    private async Task ValidateRiskAttributes(List<RiskAttribute> attributes)
    {
        List<string> attributeTypes = await _riskAttributeDao.GetAttributeTypesAsync();

        // Use a dictionary to track whether each attribute type is encountered
        Dictionary<string, bool> encounteredTypes = attributeTypes.ToDictionary(type => type, type => false);

        foreach (RiskAttribute attribute in attributes)
        {
            if (encounteredTypes.ContainsKey(attribute.AttributeType))
            {
                // Mark the attribute type as encountered
                encounteredTypes[attribute.AttributeType] = true;
            }
        }

        // Check if all attribute types have been encountered
        bool allTypesEncountered = encounteredTypes.All(kv => kv.Value);

        if (!allTypesEncountered)
        {
            // Some attribute types are missing
            throw new Exception("Attribute Validation failed - not all attributes types provided");
        }
        
    }
    
}