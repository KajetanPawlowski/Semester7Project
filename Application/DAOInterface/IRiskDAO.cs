using Domain.Model;

namespace Application.DAOInterface;

public interface IRiskDAO
{
    Task<IRisk> CreateAsync(IRisk risk);
    Task<IRisk?> GetByIdAsync(int riskId);
    Task<List<IRisk>> GetAllAsync();
}