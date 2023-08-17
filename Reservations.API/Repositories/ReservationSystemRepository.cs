using Dapper;
using Dapper.Contrib.Extensions;
using Reservations.API.Models;

namespace Reservations.API.Repositories;

public class ReservationSystemRepository : IReservationSystemsRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public ReservationSystemRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async Task<Provider> GetProvider(string externalId)
    {
        using (var connection = _sqlConnectionFactory.CreateSqlConnection())
        {
            const string sql = @"SELECT * FROM Providers WHERE ProviderExternalId = @externalId";
            return await connection.QueryFirstOrDefaultAsync<Provider>(sql, new { externalId });
        }
    }
    
    public async Task InsertSchedule(Schedule schedule)
    {
        using (var connection = _sqlConnectionFactory.CreateSqlConnection())
        {
            await connection.InsertAsync(schedule);
        }
    }

    public async Task<Schedule> GetSchedule(int providerId, string date)
    {
        using (var connection = _sqlConnectionFactory.CreateSqlConnection())
        {
            const string sql = @"SELECT * FROM Schedules WHERE ProviderId = @providerId AND Date(StartDateTime) = @date LIMIT 1";
            return await connection.QueryFirstOrDefaultAsync<Schedule>(sql, new { providerId, date });
        }
    }
    
    public async Task<Appointment> InsertAppointment(Appointment appointment)
    {
        using (var connection = _sqlConnectionFactory.CreateSqlConnection())
        {
            var id = await connection.InsertAsync(appointment);
            const string sql = @"SELECT * FROM Appointments WHERE AppointmentId = @id LIMIT 1";
            return await connection.QueryFirstOrDefaultAsync<Appointment>(sql, new { id });
        }
    }
    
    public async Task<Client> GetClient(string externalId)
    {
        using (var connection = _sqlConnectionFactory.CreateSqlConnection())
        {
            const string sql = @"SELECT * FROM Clients WHERE ClientExternalId = @externalId";
            return await connection.QueryFirstOrDefaultAsync<Client>(sql, new { externalId });
        }
    }
    
    public async Task<Schedule> GetScheduleByExternalId(string externalId)
    {
        using (var connection = _sqlConnectionFactory.CreateSqlConnection())
        {
            const string sql = @"SELECT * FROM Schedules WHERE ScheduleExternalId = @externalId";
            return await connection.QueryFirstOrDefaultAsync<Schedule>(sql, new { externalId });
        }
    }

    public async Task<Appointment> GetAppointmentByScheduleIdAndAppointmentDateTime(int scheduleId,string appointmentDateTime)
    {
        using (var connection = _sqlConnectionFactory.CreateSqlConnection())
        {
            const string sql = @"SELECT * FROM Appointments WHERE ScheduleId = @scheduleId AND AppointmentDateTime = @appointmentDateTime";
            return await connection.QueryFirstOrDefaultAsync<Appointment>(sql, new { scheduleId, appointmentDateTime });
        }
    }
}

public interface IReservationSystemsRepository
{
    Task<Provider> GetProvider(string externalId);
    Task InsertSchedule(Schedule schedule);
    Task<Schedule> GetSchedule(int providerId, string date);
    Task<Appointment> InsertAppointment(Appointment appointment);
    Task<Client> GetClient(string externalId);
    Task<Schedule> GetScheduleByExternalId(string externalId);
    Task<Appointment> GetAppointmentByScheduleIdAndAppointmentDateTime(int scheduleId, string appointmentDateTime);
}