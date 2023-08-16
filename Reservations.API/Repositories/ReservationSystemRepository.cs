using Dapper;
using Reservations.API.Models;

namespace Reservations.API.Repositories;

public class ReservationSystemRepository : IReservationSystemsRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public ReservationSystemRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async Task<Provider> GetProviders(string externalId)
    {
        using (var connection = _sqlConnectionFactory.CreateSqlConnection())
        {
            const string sql = @"SELECT * FROM Providers WHERE ProviderExternalId = @externalId";
            return await connection.QueryFirstOrDefaultAsync<Provider>(sql, new { externalId });
        }
    }
}

public interface IReservationSystemsRepository
{
    Task<Provider> GetProviders(string externalId);
    //Task InsertSchedule(Schedule schedule);
    //Task DeleteAsync(Guid externalId);
    //Task<IEnumerable<Assignments>> GetTasksByAssignee(string assignee);
    //Task UpdateAsync(Assignments assignment);
}