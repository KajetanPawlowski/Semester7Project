using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;


namespace Application.Logic;

public class RiskLogic : IRiskLogic
{
    private readonly IRiskAttributeDAO _riskAttributeDao;
    private readonly IRiskCategoryDAO _riskCategoryDao;

    public RiskLogic(IRiskAttributeDAO riskAttributeDao, IRiskCategoryDAO riskCategoryDao)
    {
        _riskAttributeDao = riskAttributeDao;
        _riskCategoryDao = riskCategoryDao;
    }
    public void CreateFromFile(string type, int score, string description)
    {
        RiskAttribute attribute = new RiskAttribute();
        attribute.AttributeType = type;
        attribute.Score = score;
        attribute.Description = description;
        _riskAttributeDao.CreateAsync(attribute);
    }
    public async Task<RiskCategory> CreateFromFile(string name)
    {
        return await _riskCategoryDao.CreateAsync(name);
    }

    public async Task<List<RiskCategory>> GetRiskCategories(RiskCategoriesSearchParamDTO dto)
    {
        return await _riskCategoryDao.GetCategoriesAsync(dto);
    }

    public async Task<RiskCategory> GetById(int id)
    {
        return await _riskCategoryDao.GetCategoryById(id);
    }
}