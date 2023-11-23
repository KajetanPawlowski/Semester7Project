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
    public async Task<ActionResult<Survey>> GetSurveysAsync(int supplierId)
    {
        try
        {
            List<Survey> surveys = await _supplierLogic.GetSurveysAsync(supplierId);
            return Ok(surveys);

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
            return Created($"/Surevy/{survey.Id}", survey);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}