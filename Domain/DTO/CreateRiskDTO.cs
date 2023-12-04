using Domain.Model;

namespace Domain.DTO;

public class CreateRiskDTO
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
}