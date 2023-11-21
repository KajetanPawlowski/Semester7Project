namespace Domain.DTO;

public class SupplierCreationDTO
{
    public int RepresentativeId { get; init; }
    public string SupplierName { get; init; }
    public string Country { get; init; }
    public int HeadCount { get; init; }
    public List<string> SuppliedProducts { get; init; }
    
}