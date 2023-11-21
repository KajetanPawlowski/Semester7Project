namespace Domain.Model;

public class Risk
{
    public int Id { get; set; }
    public string Name { get; set; }
    public RiskCategory Category {get; set; }
    public List<RiskAttribute>? RiskAttributes { get; set; }
}