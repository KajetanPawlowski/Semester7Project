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
        ISurveyDAO surveyDao, IRiskDAO riskDao, IQuestionDAO questionDao, IRiskAttributeDAO riskAttributeDao)
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
        List<Question> attributeMatch = new List<Question>();
        foreach (var relevantRisk in supplier.RelevantRisks)
        {
            foreach (var riskMapping in relevantRisk.RiskAttributes.Where(a => a.AttributeType.Equals("Risk types (mapping)")))
            {
                attributeMatch.AddRange(relevantQuestions.Where(q => GetRiskMappingAttribute(q).AttributeId == riskMapping.AttributeId));
            }

            foreach (var impactedGroup in relevantRisk.RiskAttributes.Where(a => a.AttributeType.Equals("Impacted group - most affected group")))
            {
                attributeMatch.AddRange(relevantQuestions.Where(q => GetImpactedGroupAttribute(q).AttributeId == impactedGroup.AttributeId));
            }
        }

        return attributeMatch;
    }

    private RiskAttribute GetRiskMappingAttribute(Question question)
    {
        return question.RelevantRisk.RiskAttributes.FirstOrDefault(a => a.AttributeType.Equals("Risk types (mapping)"));
    }

    private RiskAttribute GetImpactedGroupAttribute(Question question)
    {
        return question.RelevantRisk.RiskAttributes.FirstOrDefault(a => a.AttributeType.Equals("Impacted group - most affected group"));
    }

    public async Task<Survey> CreateSurvey(CreateSurveyDTO dto)
    {
        //make sure all the questions already added!!!
        //await ValidateSurveyCreationDTO(dto);
        List<Question> questions = new List<Question>();
        foreach (var questionDto in dto.Questions)
        {
            questions.Add(await AddQuestion(questionDto));
        }

        Supplier supplier = await _supplierDao.GetByIdAsync(dto.SupplierId);
        List<Survey> surveys = await _surveyDao.GetSupplierSurveysAsync(dto.SupplierId);
        string surveyName = supplier.CompanyName + " survey #" + surveys.Count;
        Survey toCreate = new()
        {
            SupplierId = dto.SupplierId,
            CreatorId = dto.CreatorId,
            CreationTime = DateTime.Now,
            Name = surveyName,
            Questions = questions
        };

        return await _surveyDao.CreateAsync(toCreate);
    }

    public Task<Survey> GetSurveyById(int surveyId)
    {
        return _surveyDao.GetByIdAsync(surveyId);
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
            RelevantRisk = dto.RelatedRisk,
            CCode = dto.CCode,
            RelevantCompanySize = dto.CompanySize
        };

        return await _questionDao.CreateAsync(toCreate);
    }

    public async Task<Survey> AnswerSurveyAsync(AnswerSurveyDTO dto)
    {
        Survey toUpdate = await _surveyDao.GetByIdAsync(dto.surveyId);
        try
        {
            foreach (var question in dto.answerQuestions)
            {
                CheckForAnswers(question);
            }

            toUpdate.Questions = dto.answerQuestions;
            toUpdate.AnsweredTime = DateTime.Now;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return await _surveyDao.UpdateAsync(toUpdate);
    }

    private async Task ValidateSurveyCreationDTO(CreateSurveyDTO dto)
    {
        //validate if the questions exist
    }

    private async Task ValidateQuestionCreationDTO(CreateQuestionDTO dto)
    {
        try
        {

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

    private void CheckForAnswers(Question question)
    {
        // Check if the answer is selected
        if (question.SelectedAnswerId == null)
        {
            // Throw an error or handle the case where the answer is not selected
            throw new Exception("Answer not selected.");
        }

    }
}