using Reservations.API.Models;
using Reservations.API.Models.Requests;
using Reservations.API.Repositories;

namespace Reservations.API.Services;

public class ScheduleService
{
    private readonly IReservationSystemsRepository _reservationSystemsRepository;
    
    public ScheduleService(IReservationSystemsRepository reservationSystemsRepository)
    {
        _reservationSystemsRepository = reservationSystemsRepository;
    }
    
    public async Task ProcessSchedule(ScheduleRequest request)
    {
        var provider = await _reservationSystemsRepository.GetProvider(request.ProviderExternalId);
        if (provider == null)
        {
            throw new Exception("Provider not found");
        }

        var schedule = new Schedule
        {
            ProviderId = provider.ProviderId,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime
        };
        
        DateTime.TryParse(schedule.StartDateTime, out var dateTime);
        DateTime dateOnly = dateTime.Date;

        // If you want the date as a string, format it as needed
        string dateString = dateOnly.ToString("yyyy-MM-dd");
        
        var existingSchedule = await _reservationSystemsRepository.GetSchedule(schedule.ProviderId, dateString);
        
        if (existingSchedule != null)
        {
            throw new Exception("Schedule already exists with the requested date");
        }
        
        await _reservationSystemsRepository.InsertSchedule(schedule);
    }
}

public interface IScheduleService
{
    Task ProcessSchedule(ScheduleRequest request);
}