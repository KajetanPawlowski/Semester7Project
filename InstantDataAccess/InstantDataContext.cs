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

    private void LoadData()
    {
        if (dataContainer != null) return;
       
        dataContainer = new ()
        {
            Suppliers = new List<Supplier>(),
                
        };
           
    }
    
}
