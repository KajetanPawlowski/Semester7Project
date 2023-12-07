using Domain.Model;

namespace InstantDataAccess;

public class InstantDataContainer
{
    public IList<Supplier> Suppliers { get; set; }

    public IList<Risk> Risks { get; set; }
    public IList<Survey> Surveys { get; set; }
    public IList<User> Users { get; set; }
    public IList<Question> Questions { get; set; }
    public IList<RiskAttribute> RiskAttributes { get; set; }
    public IList<RiskCategory> RiskCategories { get; set; }
    public IList<Country> Countries { get; set; }
    public IList<QuestionCategory> QuestionCategories { get; set; }

    
}