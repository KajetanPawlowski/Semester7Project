using System.Globalization;
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
    private readonly ICountryDAO _countryDao;
    private readonly IQuestionCategoryDAO _questionCategoryDao;
    
    public SupplierLogic(IUserDAO userDao, ISupplierDAO supplierDao, IRiskCategoryDAO riskCategoryDao, 
        ISurveyDAO surveyDao,IRiskDAO riskDao, IRiskAttributeDAO attributeDao, ICountryDAO countryDao, IQuestionCategoryDAO questionCategoryDao)
    {
        _userDao = userDao;
        _supplierDao = supplierDao;
        _riskCategoryDao = riskCategoryDao;
        _riskDao = riskDao;
        _surveyDao = surveyDao;
        _riskAttributeDao = attributeDao;
        _countryDao = countryDao;
        _questionCategoryDao = questionCategoryDao;
    }
    public async Task<Supplier> AssignNewRiskCategory(int supplierId, int categoryId)
    {
        Supplier toUpdate = await _supplierDao.GetByIdAsync(supplierId);
        RiskCategory category = await _riskCategoryDao.GetByIdAsync(categoryId);
        if (toUpdate.RiskCategories.FirstOrDefault(cat => cat.CategoryId == category.CategoryId) == null)
        {
            toUpdate.RiskCategories.Add(category);
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

        Supplier supplier = await AddQuestionCategory(toUpdate, specificRisk);
        supplier.RiskScore = await RecalculateRiskScore(supplier);

        Supplier result = await _supplierDao.UpdateAsync(supplier);
        
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

    private async Task<Supplier> AddQuestionCategory(Supplier supplier, Risk relevantRisk)
    {
        QuestionCategory newCategory = new QuestionCategory
        {
            RiskCategoryId = relevantRisk.Category.CategoryId,
            RiskMappingId = relevantRisk.RiskAttributes
                .FirstOrDefault(a => a.AttributeType.Equals("Risk types (mapping)")).AttributeId,
            ImpactedGroupId = relevantRisk.RiskAttributes.FirstOrDefault(a => a.AttributeType.Equals("Impacted group - most affected group")).AttributeId
        };
        newCategory = await _questionCategoryDao.CreateAsync(newCategory);
        
        supplier.QuestionCategories.Add(newCategory);
        return supplier;
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
            CountryCode = dto.CountryCode,
            
            RiskCategories = new List<RiskCategory>(),
            RelevantRisks = new List<Risk>(),
            QuestionCategories = new List<QuestionCategory>()
        
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
            await _countryDao.GetByCCode(dto.CountryCode);
        }
        catch (Exception e)
        {
            Console.WriteLine("Representative not found");
        }
    }

    public async Task<Country> GetCountryByCCode(string cCode)
    {
        return await _countryDao.GetByCCode(cCode);
        
    }

    public async Task<List<Country>> GetCountriesByRegion(string countryRegion)
    {
        return await _countryDao.GetByRegion(countryRegion);
    }

    public async Task<List<Country>> GetAllCountries()
    {
        return await _countryDao.GetAllAsync();
    }

    private async Task<double> RecalculateRiskScore(Supplier supplier)
    {
        const int MAX_RISK_SCORE = 16;
        const int MAX_COUNTRY_SCORE = 100;
        const double RISK_WEIGHT = 0.6;
        double countryScore = MAX_COUNTRY_SCORE - _countryDao.GetByCCode(supplier.CountryCode).Result.RiskScore;
        double highestRisk = 0;
        foreach (var risk in supplier.RelevantRisks)
        {
            double newRiskScore = CalculateRiskScore(risk);
            if (highestRisk < newRiskScore)
            {
                highestRisk = newRiskScore;
            }
        }
        double riskScoreNormalized = highestRisk / MAX_RISK_SCORE;
        double countryScoreNormalized = countryScore / MAX_COUNTRY_SCORE;

        double result = (riskScoreNormalized * RISK_WEIGHT + countryScoreNormalized * (1 - RISK_WEIGHT))*100;
        
        return result ;
    }
    private double CalculateRiskScore(Risk risk)
    {
        int sumWithLikelyhood = 0;
        foreach (var attribute in risk.RiskAttributes)
        {
            sumWithLikelyhood += attribute.Score;
        }

        RiskAttribute Likelyhood = risk.RiskAttributes.FirstOrDefault(a => a.AttributeType.Equals("Likelihood"));

        int sum = sumWithLikelyhood - Likelyhood.Score;
        double avgScore = sum / 6.0;
        return avgScore * Likelyhood.Score;
    }
    



}