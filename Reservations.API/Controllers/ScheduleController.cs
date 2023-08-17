using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Reservations.API.Models.Requests;
using Reservations.API.Services;

namespace Reservations.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : Controller
{
    private readonly IScheduleService _scheduleService;
    private readonly IValidator<ScheduleRequest> _validator;
    
    public ScheduleController(IScheduleService scheduleService, IValidator<ScheduleRequest> validator)
    {
        _scheduleService = scheduleService;
        _validator = validator;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostProviderSchedule([FromBody] ScheduleRequest request)
    {
        try
        {
            var result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => e.ErrorMessage).ToArray());
            }
            
            await _scheduleService.CreateSchedule(request);
            return Ok("Schedule processed");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "Provider Schedule not able to be created");
        }
    }
}