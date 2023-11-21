using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;

namespace Application.Logic;

public class RiskCategoryLogic : IRiskCategoryLogic
{
    private readonly IRiskCategoryDAO riskCategoryDao;

    public RiskCategoryLogic(IRiskCategoryDAO dao)
    {
        riskCategoryDao = dao;
    }
    public async Task<RiskCategory> CreateFromFile(string name)
    {
        return await riskCategoryDao.CreateAsync(name);
    }

    public async Task<List<RiskCategory>> GetRiskCategories(RiskCategoriesSearchParamDTO dto)
    {
        return await riskCategoryDao.GetCategoriesAsync(dto);
    }

    public async Task<RiskCategory> GetById(int id)
    {
        return await riskCategoryDao.GetCategoryById(id);
    }
}