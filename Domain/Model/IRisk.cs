namespace Domain.Model;

public interface IRisk
{
    public int Id { get; set; }
    public string Name { get; set; }
    public RiskCategory Category {get; set; }
}