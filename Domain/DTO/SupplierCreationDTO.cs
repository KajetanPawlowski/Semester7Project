using Domain.Model;

namespace Domain.DTO;

public class SupplierCreationDTO
{
    public int RepresentativeId { get; set; }
    public string SupplierName { get; set; }
    public string CountryCode { get; set; }
    public int HeadCount { get; set; }
    public List<string>? SuppliedProducts { get; set; }
    
}