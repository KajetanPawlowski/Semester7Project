using Domain.Model;

namespace Domain.DTO;

public class AnswerSurveyDTO
{
    public int surveyId { get; set; }
    public List<Question> answerQuestions { get; set; }
}