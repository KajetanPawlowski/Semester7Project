using Domain.Model;

namespace InstantDataAccess;

public class InstantDataContext
{
    // private const string filePath = "data.json";
    private InstantDataContainer? dataContainer;

    public IList<Supplier> Suppliers
    {
        get
        {
            LoadData();
            return dataContainer!.Suppliers;
            
        }
    }
    public IList<Question> Questions
    {
        get
        {
            LoadData();
            return dataContainer!.Questions;
            
        }
    }
    public IList<RiskAttribute> RiskAttributes
    {
        get
        {
            LoadData();
            return dataContainer!.RiskAttributes;
            
        }
    }
    public IList<RiskCategory> RiskCategories
    {
        get
        {
            LoadData();
            return dataContainer!.RiskCategories;
            
        }
    }
    public IList<Risk> Risks
    {
        get
        {
            LoadData();
            return dataContainer!.Risks;
            
        }
    }

    public IList<Survey> Surveys
    {
        get
        {
            LoadData();
            return dataContainer!.Surveys;
        }
    }

    public IList<User> Users
    {
        get
        {
            LoadData();
            return dataContainer!.Users;
        }
    }

    private void LoadData()
    {
        if (dataContainer != null) return;
       
        dataContainer = new ()
        {
            Suppliers = new List<Supplier>(),
            Risks = new List<Risk>(),
            Surveys = new List<Survey>(),
            Users = new List<User>(),
                
        };
           
    }
    
}
