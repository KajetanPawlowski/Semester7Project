namespace Domain.Model;

public class Question
{
    public int Id { get; set; }
    public string Body { get; set; }
    public List<string> AllAnswers { get; set; }
    public RiskCategory Category {get; set; }
    public Risk RelevantRisk {get; set; }
    public string CCode { get; set; }
    public int RelevantCompanySize { get; set; }
    public int? SelectedAnswerId { get; set; }
}