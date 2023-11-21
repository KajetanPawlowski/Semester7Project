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


    public Task<RiskAttribute> CreateRiskAttributeFromFile(string type, int score, string description)
    {
        throw new NotImplementedException();
    }

    public Task<RiskCategory> CreateRiskCategoryFromFile(string name)
    {
        throw new NotImplementedException();
    }

    public Task<RiskCategory> GetRiskCategoryById(int? categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<IRisk>> GetGenericRiskByCategory(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<RiskAttribute>> GetRiskAttributes()
    {
        throw new NotImplementedException();
    }

    public Task<List<RiskCategory>> GetRiskCategories(string categoryNameContent)
    {
        throw new NotImplementedException();
    }

    public Task<SpecificRisk> QualifyRisk(IRisk risk, List<RiskAttribute> attributes)
    {
        throw new NotImplementedException();
    }
}