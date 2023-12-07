namespace Domain.Model;

public class Supplier
{
    public int Id { get; set; }
    public int RepresentativeId { get; set; }
    
    public string CompanyName { get; set; }
    public int Headcount { get; set; }
    public List<string>? SuppliedProducts { get; set; }
    public string CountryCode { get; set; }
    
    public List<RiskCategory> RiskCategories { get; set; }
    public List<QuestionCategory> QuestionCategories { get; set; } 
    public List<Risk> RelevantRisks { get; set; }
}