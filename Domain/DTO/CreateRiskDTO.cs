using Domain.Model;

namespace Domain.DTO;

public class CreateRiskDTO
{
    public RiskCategory Category { get; set; }
    public string Name { get; set; }
}