using System.Numerics;
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
    
    public async Task<Survey> CreateSurvey(CreateSurveyDTO dto)
    {
        //make sure all the questions already added!!!
        await ValidateSurveyCreationDTO(dto);
        List<Question> questions = new List<Question>();
        foreach (var questionDto in dto.Questions)
        {
            questions.Add(await AddQuestion(questionDto)); 
        }

        Survey toCreate = new()
        {
            SupplierId = dto.SupplierId,
            CreatorId = dto.CreatorId,
            CreationTime = DateTime.Now,
            Name = dto.Name,
            Questions = questions
        };

        return await _surveyDao.CreateAsync(toCreate);
    }
    
    public async Task<Question> AddQuestion(CreateQuestionDTO dto)
    {
        //Add question each time new is created!!!
        await ValidateQuestionCreationDTO(dto);
        Question toCreate = new()
        {
            Body = dto.Body,
            AllAnswers = dto.AllAnswers,
            Category = dto.RiskCategory,
            RelatedRisk = dto.RelatedRisk
        };
        
        return await _questionDao.CreateAsync(toCreate);
    }
    public Task<Survey> AnswerSurveyAsync(int surveyId, Survey answeredSurvey)
    {
        throw new NotImplementedException();
    }

    private async Task ValidateSurveyCreationDTO(CreateSurveyDTO dto)
    {
        //validate if the questions exist
    }
    private async Task ValidateQuestionCreationDTO(CreateQuestionDTO dto)
    {
        try
        {
            await _riskDao.GetByIdAsync(dto.RelatedRisk.Id);
            await _riskCategoryDao.GetByIdAsync(dto.RiskCategory.CategoryId);
        }

        catch (RiskNotFound riskNotFound)
        {
            await _riskDao.CreateAsync(dto.RelatedRisk);
        }
        catch (RiskCategoryNotFound categoryNotFound)
        {
            await _riskCategoryDao.CreateAsync(dto.RiskCategory);
        }
        
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}