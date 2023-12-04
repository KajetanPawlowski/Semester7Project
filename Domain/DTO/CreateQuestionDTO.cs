using Domain.Model;

namespace Domain.DTO;

public class CreateQuestionDTO
{
    //IF ID == -1 - add new question to the list
    public int Id { get; set; }
    public string Body { get; set; }
    public List<string> AllAnswers { get; set; }
    public RiskCategory RiskCategory { get; set; }
    public Risk RelatedRisk { get; set; }
    
    public string CCode { get; set; }
    public int CompanySize { get; set; }
    
}