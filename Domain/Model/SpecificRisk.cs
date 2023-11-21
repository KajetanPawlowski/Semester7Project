namespace Domain.Model;

public class SpecificRisk : IRisk
{
    public int Id { get; set; }
    public string Name { get; set; }
    public RiskCategory Category {get; set; }
    public List<RiskAttribute> RiskAttributes { get; set; }
}