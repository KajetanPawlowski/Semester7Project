using Application.Logic;
using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SupplierController : ControllerBase
{
    private readonly ISupplierLogic _supplierLogic;

    public SupplierController(ISupplierLogic supplierLogic)
    {
        _supplierLogic = supplierLogic;
    }
    //Get Suppliers
    [HttpGet,  AllowAnonymous]
    public async Task<ActionResult<List<Supplier>>> GetSuppliersAsync()
    {
        try
        {
            List<Supplier> suppliers = await _supplierLogic.GetSuppliersAsync();
            return Ok(suppliers);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    //Post Supplier
    [HttpPost,  AllowAnonymous]
    public async Task<ActionResult<Supplier>> CreateAsync(SupplierCreationDTO dto)
    {
        try
        {
            Supplier supplier = await _supplierLogic.CreateSupplierAsync(dto);
            return Created($"/Supplier/{supplier.Id}", supplier);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}