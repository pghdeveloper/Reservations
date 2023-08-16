using Reservations.API.Models;
using Reservations.API.Models.Requests;
using Reservations.API.Repositories;

namespace Reservations.API.Services;

public class ScheduleService : IScheduleService
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
            StartDateTime = request.StartDateTime.ToString("yyyy-MM-dd hh:mm:ss"),
            EndDateTime = request.EndDateTime.ToString("yyyy-MM-dd hh:mm:ss"),
            ScheduleExternalId = Guid.NewGuid().ToString()
        };
        
        DateTime.TryParse(schedule.StartDateTime, out var dateTime);
        var dateOnly = dateTime.Date;

        // If you want the date as a string, format it as needed
        var dateString = dateOnly.ToString("yyyy-MM-dd");
        
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