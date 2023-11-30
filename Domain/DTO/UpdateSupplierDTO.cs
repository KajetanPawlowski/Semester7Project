using Domain.Model;

namespace Domain.DTO;

public class UpdateSupplierDTO
{
    public int SupplierId { get; set; }
    public List<int>? CategoriesId { get; set; }
    public List<Risk>? Risks { get; set; }
}