using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;

namespace Application.Logic;

public class SupplierLogic : ISupplierLogic
{
    private readonly IUserDAO _userDao;
    private readonly IRiskCategoryDAO _riskCategoryDao;
    private readonly IRiskDAO _riskDao;
    private readonly ISupplierDAO _supplierDao;
    private readonly ISurveyDAO _surveyDao;
    private readonly IRiskAttributeDAO _riskAttributeDao;
    
    public SupplierLogic(IUserDAO userDao, ISupplierDAO supplierDao, IRiskCategoryDAO riskCategoryDao, 
        ISurveyDAO surveyDao,IRiskDAO riskDao, IRiskAttributeDAO attributeDao)
    {
        _userDao = userDao;
        _supplierDao = supplierDao;
        _riskCategoryDao = riskCategoryDao;
        _riskDao = riskDao;
        _surveyDao = surveyDao;
        _riskAttributeDao = attributeDao;
    }
    public async Task<Supplier> AssignNewRiskCategory(int supplierId, int categoryId)
    {
        Supplier toUpdate = await _supplierDao.GetByIdAsync(supplierId);
        RiskCategory category = await _riskCategoryDao.GetByIdAsync(categoryId);
        if (toUpdate.Categories.FirstOrDefault(cat => cat.CategoryId == category.CategoryId) == null)
        {
            toUpdate.Categories.Add(category);
        }
        return await _supplierDao.UpdateAsync(toUpdate);
    }

    public async Task<Supplier> AssignSpecificRisk(int supplierId, Risk specificRisk)
    {
        Supplier toUpdate = await _supplierDao.GetByIdAsync(supplierId);
        await ValidateRiskAttributes(specificRisk.RiskAttributes);

        if (specificRisk.Id == 0)
        {
            specificRisk = await _riskDao.CreateAsync(specificRisk);
        }

        Risk existingRisk = toUpdate.RelevantRisks.FirstOrDefault(risk => risk.Id == specificRisk.Id);

        if (existingRisk == null)
        {
            toUpdate.RelevantRisks.Add(specificRisk);
        }
        else
        {
            // Use the index to update the existing risk
            int index = toUpdate.RelevantRisks.IndexOf(existingRisk);
            toUpdate.RelevantRisks[index] = specificRisk;
        }

        Supplier result = await _supplierDao.UpdateAsync(toUpdate);
        return result;
    }
    
    private async Task ValidateRiskAttributes(List<RiskAttribute> attributes)
    {
        List<string> attributeTypes = await _riskAttributeDao.GetAttributeTypesAsync();
        if (attributeTypes.Count != attributes.Count)
        {
            throw new Exception("Attribute Validation failed - wrong No of attributes");
        }

        // Use a dictionary to track whether each attribute type is encountered
        Dictionary<string, bool> encounteredTypes = attributeTypes.ToDictionary(type => type, type => false);

        foreach (RiskAttribute attribute in attributes)
        {
            if (encounteredTypes.ContainsKey(attribute.AttributeType))
            {
                // Mark the attribute type as encountered
                encounteredTypes[attribute.AttributeType] = true;
            }
        }

        // Check if all attribute types have been encountered
        bool allTypesEncountered = encounteredTypes.All(kv => kv.Value);

        if (!allTypesEncountered)
        {
            // Some attribute types are missing
            throw new Exception("Attribute Validation failed - not all attributes types provided");
        }
        
    }
    public Task<Supplier> GetSupplierByMail(string supplierMail)
    {
        return _supplierDao.GetByIdAsync(_userDao.GetByMailAsync(supplierMail).Id);
    }

    public Task<Supplier> GetSupplierById(int supplierId)
    {
        return _supplierDao.GetByIdAsync(supplierId);
    }

    public Task<List<Survey>> GetSurveysAsync(int supplierId)
    {
        return _surveyDao.GetSupplierSurveysAsync(supplierId);
    }

    public async Task<Supplier> CreateSupplierAsync(SupplierCreationDTO dto)
    {
        await ValidateSupplierCreationDTO(dto);
        Supplier toCreate = new()
        {
            CompanyName = dto.SupplierName,
            RepresentativeId = dto.RepresentativeId,
            SuppliedProducts = dto.SuppliedProducts,
            Headcount = dto.HeadCount,
            Country = dto.Country,
            
            Categories = new List<RiskCategory>(),
            RelevantRisks = new List<Risk>()
        
        };
        return await _supplierDao.CreateAsync(toCreate);
    }

    public Task<List<Supplier>> GetSuppliersAsync()
    {
        return _supplierDao.GetAllAsync();
    }

    private async Task ValidateSupplierCreationDTO(SupplierCreationDTO dto)
    {
        try
        {
            await _userDao.GetByIdAsync(dto.RepresentativeId);
        }
        catch (Exception e)
        {
            Console.WriteLine("Representative not found");
        }
    }
    
    
    
}