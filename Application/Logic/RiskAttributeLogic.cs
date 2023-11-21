using Application.DAOInterface;
using Application.LogicInterface;
using Domain.Model;


namespace Application.Logic;

public class RiskAttributeLogic : IRiskAttributeLogic
{
    private readonly IRiskAttributeDAO riskAttributeDao;

    public RiskAttributeLogic(IRiskAttributeDAO dao)
    {
        riskAttributeDao = dao;
    }
    public void CreateFromFile(string type, int score, string description)
    {
        RiskAttribute attribute = new RiskAttribute();
        attribute.AttributeType = type;
        attribute.Score = score;
        attribute.Description = description;
        riskAttributeDao.CreateAsync(attribute);
    }
}