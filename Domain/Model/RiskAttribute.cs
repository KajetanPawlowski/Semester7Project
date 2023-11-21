namespace Domain.Model;

public class RiskAttribute
{
    public int AttributeId { get; set; }
    public string? AttributeType { get; set; }
    public int Score { get; set; }
    public string? Description { get; set; }
}