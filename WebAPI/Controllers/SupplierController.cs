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
    
    //Get Suppliers by Id
    [HttpGet,  AllowAnonymous]
    public async Task<ActionResult<List<Supplier>>> GetSupplierByIdAsync([FromQuery] int? supplierId)
    {
        try
        {
            List<Supplier> suppliers = new List<Supplier>();
            if (supplierId == null)
            {
                suppliers = await _supplierLogic.GetSuppliersAsync();
            }
            else
            {
                suppliers.Add(await _supplierLogic.GetSupplierById((int)supplierId));
            }
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
    //Patch Supplier
    [HttpPatch, AllowAnonymous]
    public async Task<ActionResult<Supplier>> UpdateAsync(UpdateSupplierDTO dto)
    {
        try
        {
            Supplier supplier = await _supplierLogic.GetSupplierById(dto.SupplierId);
            if (dto.CategoriesId != null)
            {
                foreach (var catId in dto.CategoriesId)
                {
                    supplier = await _supplierLogic.AssignNewRiskCategory(dto.SupplierId, catId);
                }
            }

            if (dto.Risks != null)
            {
                foreach (var risk in dto.Risks)
                {
                    supplier = await _supplierLogic.AssignSpecificRisk(dto.SupplierId, risk);
                }
            }
            return Created($"/Supplier/{supplier.Id}", supplier);

        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}