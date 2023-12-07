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
    private readonly IQuestionCategoryDAO _questionCategoryDao;
    private readonly ISupplierDAO _supplierDao;
    private readonly ISurveyDAO _surveyDao;
    private readonly IQuestionDAO _questionDao;

    public SurveyLogic(IUserDAO userDao, ISupplierDAO supplierDao, IRiskCategoryDAO riskCategoryDao,
        ISurveyDAO surveyDao, IQuestionDAO questionDao, IRiskAttributeDAO riskAttributeDao, IQuestionCategoryDAO questionCategoryDao)
    {
        _userDao = userDao;
        _supplierDao = supplierDao;
        _riskCategoryDao = riskCategoryDao;
        _questionCategoryDao = questionCategoryDao;
        _surveyDao = surveyDao;
        _questionDao = questionDao;
        _riskAttributeDao = riskAttributeDao;
        
    }

    public async Task<List<Question>> GenerateQuestions(int supplierId)
    {
        Supplier supplier = await _supplierDao.GetByIdAsync(supplierId);
        List<Question> relevantQuestions = new List<Question>();
        foreach (QuestionCategory category in supplier.QuestionCategories)
        {
            relevantQuestions.AddRange(await _questionDao.GetByCategory(category));
        }
        return relevantQuestions.Distinct().ToList();
    }
    

    public async Task<Survey> CreateSurvey(CreateSurveyDTO dto)
    {
        //make sure all the questions already added!!!
        //await ValidateSurveyCreationDTO(dto);
        List<Question> questions = new List<Question>();
        foreach (var questionDto in dto.Questions)
        {
            //if ID -1 = request to add brand new question
            if (questionDto.Id < 0)
            {
                questions.Add(await AddQuestion(questionDto));
            }
            else
            {
                questions.Add(await _questionDao.GetByIdAsync(questionDto.Id));
            }
            
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
        Question toCreate = new()
        {
            Body = dto.Body,
            AllAnswers = dto.AllAnswers,
            QuestionCategory = dto.QuestionCategory,
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