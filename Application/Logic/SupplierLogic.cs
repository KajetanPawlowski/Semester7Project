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
    
    public SupplierLogic(IUserDAO userDao, ISupplierDAO supplierDao, IRiskCategoryDAO riskCategoryDao, 
        ISurveyDAO surveyDao,IRiskDAO riskDao)
    {
        _userDao = userDao;
        _supplierDao = supplierDao;
        _riskCategoryDao = riskCategoryDao;
        _riskDao = riskDao;
        _surveyDao = surveyDao;
    }
    public async Task<Supplier> AssignNewRiskCategory(int supplierId, int categoryId)
    {
        Supplier toUpdate = await _supplierDao.GetByIdAsync(supplierId);
        RiskCategory category = await _riskCategoryDao.GetByIdAsync(categoryId);
        if (!toUpdate.Categories.Contains(category))
        {
            toUpdate.Categories.Add(category);
        }
        return await _supplierDao.UpdateAsync(toUpdate);
    }

    public async Task<Supplier> AssignSpecificRisk(int supplierId, Risk specificRisk)
    {
        Supplier toUpdate = await _supplierDao.GetByIdAsync(supplierId);
        if (specificRisk.Id == 0)
        {
            await _riskDao.CreateAsync(specificRisk);
        }
        toUpdate.RelevantRisks.Add(specificRisk);
        Supplier result = await _supplierDao.UpdateAsync(toUpdate);
        return result;
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