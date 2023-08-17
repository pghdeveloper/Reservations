using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Reservations.API.Models.Requests;
using Reservations.API.Services;

namespace Reservations.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AppointmentController : Controller
{
    private readonly IAppointmentService _appointmentService;
    private readonly IValidator<AppointmentRequest> _validator;
    
    public AppointmentController(IAppointmentService appointmentService, IValidator<AppointmentRequest> validator)
    {
        _validator = validator;
        _appointmentService = appointmentService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AppointmentRequest request)
    {
        try
        {
            var result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => e.ErrorMessage).ToArray());
            }
            
            var appointment = await _appointmentService.CreateAppointment(request);
            
            //This is where we would send the appointment info to a queue for processing.  It would have a delay of 30 minutes
            //The consumer would then pick up the message from the queue and process it.  It would check if the appointment has been confirmed
            //If already confirmed, we do nothing, else we make a call to the db to update the expired field to true in the appointments table for that appointment
            //Because of time constraints, I was not able to implement this.
            
            return Ok(appointment);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "Appointment not able to be created");
        }
    }
}