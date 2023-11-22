using Application.DAOInterface;
using Domain.Model;

namespace InstantDataAccess.DAO;

public class SurveyInstantDAO : ISurveyDAO
{
    private readonly InstantDataContext context;
    
    public SurveyInstantDAO(InstantDataContext context)
    {
        this.context = context;
    }
    
    public Task<Survey> CreateAsync(Survey survey)
    {
        throw new NotImplementedException();
    }

    public Task<Survey> GetByIdAsync(int surveyId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Survey>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Survey>> GetSupplierSurveysAsync(int supplierId)
    {
        throw new NotImplementedException();
    }

    public Task<Survey> UpdateAsync(Survey survey)
    {
        throw new NotImplementedException();
    }
}