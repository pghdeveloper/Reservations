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

    public async Task<Schedule> GetSchedule(int id, string date)
    {
        using (var connection = _sqlConnectionFactory.CreateSqlConnection())
        {
            const string sql = @"SELECT * FROM Schedules WHERE ProviderId = @id AND Date(StartDateTime) = @date LIMIT 1";
            return await connection.QueryFirstOrDefaultAsync<Schedule>(sql, new { id, date });
        }
    }
}

public interface IReservationSystemsRepository
{
    Task<Provider> GetProvider(string externalId);
    Task InsertSchedule(Schedule schedule);
    Task<Schedule> GetSchedule(int id, string date);
    //Task DeleteAsync(Guid externalId);
    //Task<IEnumerable<Assignments>> GetTasksByAssignee(string assignee);
    //Task UpdateAsync(Assignments assignment);
}