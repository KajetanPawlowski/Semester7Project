using Application.DAOInterface;
using Domain.Model;

namespace InstantDataAccess.DAO;

public class CountryInstantDAO : ICountryDAO
{
    private readonly InstantDataContext context;

    public CountryInstantDAO(InstantDataContext context)
    {
        this.context = context;
    }
    public async Task<Country> CreateAsync(Country country)
    {
        context.Countries.Add(country);
        context.SaveChanges();

        return await Task.FromResult(country);
    }
    public async Task<Country> GetByCCode(string cCode)
    {
        Country? existing = context.Countries.FirstOrDefault(c =>
            c.CCode == cCode);
        if(existing == null)
        {
            throw new Exception("Country not found");
        }

        return await Task.FromResult(existing);
    }

    public async Task<List<Country>> GetAllAsync()
    {
        return await Task.FromResult(context.Countries.ToList());
    }

    public async Task<List<Country>> GetByRegion(string countryRegion)
    {
        List<Country> countries = context.Countries
            .Where(c =>c.Region.Equals(countryRegion)).ToList();

        return await Task.FromResult(countries);
    }
}