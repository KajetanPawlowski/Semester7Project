using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IRiskCategoryDAO riskCategoryDao;

    public UserLogic(IRiskCategoryDAO dao)
    {
        riskCategoryDao = dao;
    }
    
}