using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SurveyController : ControllerBase
{
    private readonly ISurveyLogic _surveyLogic;
    private readonly ISupplierLogic _supplierLogic;

    public SurveyController(ISurveyLogic surveyLogic, ISupplierLogic supplierLogic)
    {
        _surveyLogic = surveyLogic;
        _supplierLogic = supplierLogic;
    }
    
    //Get Surveys
    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<Survey>> GetSurveysAsync(int? supplierId, int? surveyId)
    {
        try
        {
            List<Survey> surveys = new List<Survey>();
            if (supplierId != null)
            {
                string temp = supplierId + "";
                surveys = await _supplierLogic.GetSurveysAsync(int.Parse(temp));
            }
            else
            {
                string temp = surveyId + "";
                surveys.Add(await _surveyLogic.GetSurveyById(int.Parse(temp)));   
            }
            
            return Ok(surveys);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    //Get Questions
    [HttpGet("Question")]
    [AllowAnonymous]
    public async Task<ActionResult<Question>> GetGenericQuestionsAsync([FromQuery]int supplierId)
    {
        try
        {
            List<Question> questions = await _surveyLogic.GenerateQuestions(supplierId);
            return Ok(questions);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    //Post Survey
    [HttpPost, AllowAnonymous]
    public async Task<ActionResult<Survey>> CreateAsync(CreateSurveyDTO dto)
    {
        try
        {
            Survey survey = await _surveyLogic.CreateSurvey(dto);
            return Created($"/Survey/{survey.Id}", survey);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch, AllowAnonymous]
    public async Task<ActionResult<Survey>> AnswerSurvey(AnswerSurveyDTO dto)
    {
        try
        {
            Survey survey = await _surveyLogic.AnswerSurveyAsync(dto);
            return Ok(survey);

        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    
    }
    
}