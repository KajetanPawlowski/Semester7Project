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
        Risk genericRisk = new Risk
        {
            Name = riskName,
            Category = await GetRiskCategoryById(riskCategoryId)
        };
        return await _riskDao.CreateAsync(genericRisk);

    }

    public Task<RiskAttribute> CreateRiskAttributeFromFile(string type, int score, string description)
    {
        RiskAttribute attribute = new RiskAttribute
        {
            AttributeType = type,
            Score = score,
            Description = description
        };
        return _riskAttributeDao.CreateAsync(attribute);
    }

    public Task<RiskCategory> CreateRiskCategoryFromFile(string name)
    {
        RiskCategory category = new()
        {
            CategoryName = name
        };
        return _riskCategoryDao.CreateAsync(category);
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

    public Task<List<RiskAttribute>> GetRiskAttributes()
    {
        return _riskAttributeDao.GetAllAsync();
    }

    public Task<List<RiskCategory>> GetRiskCategories()
    {
        return _riskCategoryDao.GetAllAsync();
    }

    public async Task<Risk> QualifyRisk(Risk risk, List<RiskAttribute> attributes)
    {
        await _riskDao.GetByIdAsync(risk.Id);
        risk.RiskAttributes = attributes;
        return await _riskDao.UpdateRisk(risk);
    }

    
    
}