using Domain.Model;

namespace Domain.DTO;

public class CreateQuestionDTO
{
    //IF ID == -1 - add new question to the list
    public int Id { get; set; }
    public string Body { get; set; }
    public List<string> AllAnswers { get; set; }
    public QuestionCategory QuestionCategory { get; set; }
    
    public string CCode { get; set; }
    public int CompanySize { get; set; }
    
}