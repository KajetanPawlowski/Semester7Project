namespace Domain.Model;

public class GenericRisk : IRisk
{
    public int Id { get; set; }
    public string Name { get; set; }
    public RiskCategory Category {get; set; }
}