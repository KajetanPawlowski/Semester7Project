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
        int surveyId = 1;
        if (context.Surveys.Any())
        {
            surveyId = context.Surveys.Max(s => s.Id);
            surveyId++;
        }

        survey.Id = surveyId;
        context.Surveys.Add(survey);
        context.SaveChanges();

        return Task.FromResult(survey);
    }

    public async Task<Survey> GetByIdAsync(int surveyId)
    {
        Survey? existing = context.Surveys.FirstOrDefault(s =>
            s.Id == surveyId);
        if(existing == null)
        {
            throw new Exception("Survey not found");
        }

        return await Task.FromResult(existing);
    }

    public Task<List<Survey>> GetAllAsync()
    {
        return Task.FromResult(context.Surveys.ToList());
    }

    public Task<List<Survey>> GetSupplierSurveysAsync(int supplierId)
    {
        List<Survey> surveys = context.Surveys
            .Where(s =>s.SupplierId == supplierId).ToList();

        return Task.FromResult(surveys);
    }

    public Task<Survey> UpdateAsync(Survey survey)
    {
        Survey? existing = context.Surveys.FirstOrDefault(s => s.Id== survey.Id);
        if (existing == null)
        {
            throw new Exception("Survey not found");
        }
        context.Surveys.Remove(existing);
        context.Surveys.Add(survey);
        
        
        context.SaveChanges();
        return (Task<Survey>)Task.CompletedTask;
    }
}