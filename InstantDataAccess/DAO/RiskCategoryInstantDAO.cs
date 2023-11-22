using Application.DAOInterface;
using Domain.Model;

namespace InstantDataAccess.DAO;

public class RiskCategoryInstantDAO : IRiskCategoryDAO
{
    private readonly InstantDataContext context;

    public RiskCategoryInstantDAO(InstantDataContext context)
    {
        this.context = context;
    }
    public Task<RiskCategory> CreateAsync(RiskCategory category)
    {
        throw new NotImplementedException();
    }

    public Task<RiskCategory> GetByIdAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<RiskCategory>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}