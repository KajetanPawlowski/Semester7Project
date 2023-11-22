using Domain.Model;

namespace InstantDataAccess;

public class InstantDataContainer
{
    public IList<Supplier> Suppliers { get; set; }
    public IList<Risk> Risks { get; set; }
    public IList<Survey> Surveys { get; set; }
    public IList<User> Users { get; set; }
    
    
}