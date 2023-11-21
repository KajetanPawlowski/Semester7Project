using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTO;
using Domain.Exeptions;
using Domain.Model;

namespace Application.Logic;

public class SurveyLogic : ISurveyLogic
{
    private readonly IUserDAO _userDao;
    private readonly IRiskCategoryDAO _riskCategoryDao;
    private readonly IRiskAttributeDAO _riskAttributeDao;
    private readonly IRiskDAO _riskDao;
    private readonly ISupplierDAO _supplierDao;
    private readonly ISurveyDAO _surveyDao;
    private readonly IQuestionDAO _questionDao;
    public SurveyLogic(IUserDAO userDao, ISupplierDAO supplierDao, IRiskCategoryDAO riskCategoryDao, 
        ISurveyDAO surveyDao,IRiskDAO riskDao, IQuestionDAO questionDao, IRiskAttributeDAO riskAttributeDao)
    {
        _userDao = userDao;
        _supplierDao = supplierDao;
        _riskCategoryDao = riskCategoryDao;
        _riskDao = riskDao;
        _surveyDao = surveyDao;
        _questionDao = questionDao;
        _riskAttributeDao = riskAttributeDao;
    }
    public async Task<List<Question>> GenerateQuestions(int supplierId)
    {
        Supplier supplier = await _supplierDao.GetByIdAsync(supplierId);
        List<Question> relevantQuestions = new List<Question>();
        foreach (RiskCategory category in supplier.Categories)
        {
            relevantQuestions.AddRange(await _questionDao.GetByCategory(category));
        }

        foreach (Risk risk in supplier.RelevantRisks)
        {
            if (risk.RiskAttributes != null)
            {
                foreach (RiskAttribute attribute in risk.RiskAttributes)
                {
                    relevantQuestions.AddRange(await _questionDao.GetByAttribute(attribute));
                }
            }
        }
        return relevantQuestions;
    }

    public async Task<Question> AddQuestion(CreateQuestionDTO dto)
    {
        await ValidateQuestionCreationDTO(dto);
        Question toCreate = new()
        {
            Body = dto.Body,
            AllAnswers = dto.AllAnswers,
            Category = dto.RiskCategory,
            RelatedRisk = dto.RelatedRisk
        };
        Question result = await _questionDao.CreateAsync(toCreate);
        Survey toUpdate = await _surveyDao.GetByIdAsync(dto.SurveyId);
        
        toUpdate.Questions.Add(result);
        await _surveyDao.UpdateAsync(toUpdate);

        return result;
    }

    public Task<Survey> CreateSurvey(CreateSurveyDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<Survey> AnswerSurveyAsync(int surveyId, Survey answeredSurvey)
    {
        throw new NotImplementedException();
    }

    private async Task ValidateQuestionCreationDTO(CreateQuestionDTO dto)
    {
        try
        {
            await _supplierDao.GetByIdAsync(dto.SupplierId);
            await _surveyDao.GetByIdAsync(dto.SurveyId);
            await _riskDao.GetByIdAsync(dto.RelatedRisk.Id);
            await _riskCategoryDao.GetByIdAsync(dto.RiskCategory.CategoryId);
        }
        catch (SurveyNotFound surveyNotFound)
        {
            //create new survey using supplier
        }
        catch (RiskNotFound riskNotFound)
        {
            await _riskDao.CreateAsync(dto.RelatedRisk);
        }
        catch (RiskCategoryNotFound categoryNotFound)
        {
            await _riskCategoryDao.CreateAsync(dto.RiskCategory);
        }
    }
}