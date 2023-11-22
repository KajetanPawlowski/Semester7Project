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
    private void LoadData()
    {
        if (dataContainer != null) return;
       
        dataContainer = new ()
        {
            Suppliers = new List<Supplier>(),
                
        };
           
    }
    
}
