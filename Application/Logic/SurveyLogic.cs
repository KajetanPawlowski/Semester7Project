using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;

namespace Application.Logic;

public class SurveyLogic : ISurveyLogic
{
    public Task<List<Question>> GenerateQuestions(int supplierId)
    {
        throw new NotImplementedException();
    }

    public Task<Survey> CreateSurvey(CreateSurveyDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<Survey> AnswerSurveyAsync(int surveyId, Survey answeredSurvey)
    {
        throw new NotImplementedException();
    }
}