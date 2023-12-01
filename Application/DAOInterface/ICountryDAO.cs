using Domain.Model;

namespace Application.DAOInterface;

public interface ICountryDAO
{
    Task<Country> GetByCCode(string cCode);
    Task<List<Country>> GetAllAsync();
    Task<List<Country>> GetByRegion(string countryRegion);
    Task<Country> CreateAsync(Country country);
}