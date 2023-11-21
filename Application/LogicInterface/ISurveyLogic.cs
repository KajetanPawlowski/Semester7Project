using Domain.DTO;
using Domain.Model;

namespace Application.LogicInterface;

public interface ISurveyLogic
{
    Task<List<Question>> GenerateQuestions(int supplierId);
    Task<Survey> CreateSurvey(CreateSurveyDTO dto);
    Task<Question> AddQuestion(CreateQuestionDTO dto);
    Task<Survey> AnswerSurveyAsync(int surveyId, Survey answeredSurvey);
    
}