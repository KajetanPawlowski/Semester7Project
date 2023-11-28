using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class RiskController : ControllerBase
{
    private readonly IRiskLogic _riskLogic;

    public RiskController(IRiskLogic riskLogic)
    {
        _riskLogic = riskLogic;
    }
    //POST risk
    [HttpPost,  AllowAnonymous]
    public async Task<ActionResult<Risk>> CreateAsync(CreateRiskDTO dto)
    {
        try
        {
            Risk risk = await _riskLogic.CreateRisk(dto.Name, dto.Category.CategoryId);
            return Created($"/Risk/{risk.Id}", risk);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    //Get RiskCategory
    [HttpGet ("Category")]
    [AllowAnonymous]
    public async Task<ActionResult<List<RiskCategory>>> GetCategoryByIdAsync([FromQuery] int? categoryId)
    {
        try
        {
            List<RiskCategory> categories = new List<RiskCategory>();
            if (categoryId == null)
            {
                categories = await _riskLogic.GetRiskCategories();
            }
            else
            {
                string temp = categoryId + "";
                categories.Add(await _riskLogic.GetRiskCategoryById(int.Parse(temp)));
            }
            return Ok(categories);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    //Get Risk Attributes
    [HttpGet ("Attribute")]
    [AllowAnonymous]
    public async Task<ActionResult<List<RiskAttribute>>> GetAttributesAsync([FromQuery] string? type)
    {
        try
        {
            List<RiskAttribute> attributes = new List<RiskAttribute>();
            if (type == null)
            {
                attributes = await _riskLogic.GetRiskAttributes();
            }
            else
            {
                attributes = await _riskLogic.GetRiskAttributesByType(type);
            }
            return Ok(attributes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}