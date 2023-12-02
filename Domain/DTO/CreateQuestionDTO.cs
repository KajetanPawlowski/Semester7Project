using Domain.Model;

namespace Domain.DTO;

public class CreateQuestionDTO
{
    public string Body { get; set; }
    public List<string> AllAnswers { get; set; }
    public RiskCategory RiskCategory { get; set; }
    public Risk RelatedRisk { get; set; }
    
    public string CCode { get; set; }
    public int CompanySize { get; set; }
    
}