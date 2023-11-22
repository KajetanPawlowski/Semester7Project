using Domain.Model;

namespace InstantDataAccess;

public class InstantDataContainer
{
    public IList<Supplier> Suppliers { get; set; }
    public IList<Question> Questions { get; set; }
    public IList<RiskAttribute> RiskAttributes { get; set; }
    public IList<RiskCategory> RiskCategories { get; set; }
    
}