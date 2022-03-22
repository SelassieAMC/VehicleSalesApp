using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleSales.Application.Interfaces;

namespace VehicleSales.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VehicleSalesController : ControllerBase
{
    private readonly IVehicleSalesDataService _vehicleSalesDataService;

    public VehicleSalesController(IVehicleSalesDataService vehicleSalesDataService)
    {
        _vehicleSalesDataService = vehicleSalesDataService;
    }

    [HttpPost("upload-from-csv")]
    public async Task<IActionResult> UploadVehicleSalesFromCsv(IFormFile file)
    {
        try
        {
            var errors = await _vehicleSalesDataService.UploadVehicleSalesFromCsvAsync(file);
            if (errors.Any())
                return new OkObjectResult(errors);
        }
        catch (FormatException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }

        return Ok();
    }
    
    [HttpGet]
    public IActionResult GetVehicleSales()
    {
        try
        {
            var data = _vehicleSalesDataService.GetVehicleSalesData();
            return new OkObjectResult(data);
        }
        catch (Exception)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
