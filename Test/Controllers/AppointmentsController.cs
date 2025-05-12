using Microsoft.AspNetCore.Mvc;
using Test.Exceptions;
using Test.Model.Appointment;
using Test.Services;

namespace Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAppointmentById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _appointmentService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (InvalidAppointmentIdException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (NoSuchAppointmentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error occured.", detail = ex.Message});
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewAppointment ([FromBody] CreateAppointmentDTO createAppointmentDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var id = await _appointmentService.CreateNewAppointment(createAppointmentDto, cancellationToken);
            return CreatedAtAction(nameof(CreateNewAppointment), new { id }, new { id });
        }
        //TODO: catch all needed exceptions
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error occured.", detail = ex.Message });
        }
    }
}