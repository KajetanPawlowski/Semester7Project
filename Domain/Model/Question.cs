namespace Domain.Model;

public class Question
{
    public int Id { get; set; }
    public string Body { get; set; }
    public List<string> AllAnswers { get; set; }
    public RiskCategory? Category {get; set; }
    public List<RiskAttribute>? RiskAttributes { get; set; }
    public int? SelectedAnswerId { get; set; }
}