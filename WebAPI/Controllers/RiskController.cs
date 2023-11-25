using Application.LogicInterface;
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
    //Get RiskCategory
    [HttpGet ("Category")]
    [AllowAnonymous]
    public async Task<ActionResult<List<RiskCategory>>> GetCategoryByIdAsync([FromQuery] int? categoryId)
    {
        try
        {
            List<RiskCategory> suppliers = new List<RiskCategory>();
            if (categoryId == null)
            {
                suppliers = await _riskLogic.GetRiskCategories();
            }
            else
            {
                throw new NotImplementedException();
            }
            return Ok(suppliers);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    
}