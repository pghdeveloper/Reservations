using Reservations.API.Models;
using Reservations.API.Models.Requests;
using Reservations.API.Models.Response;
using Reservations.API.Repositories;

namespace Reservations.API.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IReservationSystemsRepository _reservationSystemsRepository;
    
    public AppointmentService(IReservationSystemsRepository reservationSystemsRepository)
    {
        _reservationSystemsRepository = reservationSystemsRepository;
    }
    
    public async Task<AppointmentResponse> CreateAppointment(AppointmentRequest request)
    {
        var client = await _reservationSystemsRepository.GetClient(request.ClientExternalId);
        if (client == null)
        {
            throw new Exception("Client not found");
        }

        var schedule = await _reservationSystemsRepository.GetScheduleByExternalId(request.ScheduleExternalId);
        if (schedule == null)
        {
            throw new Exception("Schedule not found");
        }
        
        if (request.AppointmentDateTime < DateTime.Parse(schedule.StartDateTime) || request.AppointmentDateTime > DateTime.Parse(schedule.EndDateTime))
        {
            throw new Exception("AppointmentDateTime must be within the schedule start and end times");
        }

        var appointmentDateTime = request.AppointmentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        var existingAppointment = await _reservationSystemsRepository.GetAppointmentByScheduleIdAndAppointmentDateTime(schedule.ScheduleId, appointmentDateTime);

        if (existingAppointment != null)
        {
            throw new Exception("Appointment already exists with the requested date");
        }

        var createdDateTime = DateTimeOffset.UtcNow;
        var expirationDateTime = createdDateTime.AddMinutes(30);
        
        var appointment = new Appointment
        {
            ScheduleId = schedule.ScheduleId,
            ClientId = client.ClientId,
            AppointmentExternalId = Guid.NewGuid().ToString(),
            AppointmentDateTime = appointmentDateTime,
            CreatedDateTime = createdDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
            ExpirationDateTime = expirationDateTime.ToString("yyyy-MM-ddTHH:mm:ss")
        };
        
        await _reservationSystemsRepository.InsertAppointment(appointment);

        return new AppointmentResponse
        {
            AppointmentDateTime = request.AppointmentDateTime,
            ScheduleExternalId = request.ScheduleExternalId,
            ClientExternalId = request.ClientExternalId,
            Confirmed = appointment.Confirmed,
            Expired = appointment.Expired,
            AppointmentExternalId = appointment.AppointmentExternalId
        };
    }
    
    public async Task ConfirmAppointment(string appointmentExternalId)
    {
        await _reservationSystemsRepository.ConfirmAppointment(appointmentExternalId);
    }
}

public interface IAppointmentService
{
    Task<AppointmentResponse> CreateAppointment(AppointmentRequest request);
    Task ConfirmAppointment(string appointmentExternalId);
}