using System.Data.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace KanbanManagement.API.Shared.HealthCheck.Extension
{
    public class NpgsqlHealthCheck : IHealthCheck
    {
        private static readonly string DefaultTestQuery = "Select 1";
        public string ConnectionString { get; }
        public string TestQuery { get; }
        public NpgsqlHealthCheck(string connectionString)
            : this(connectionString, testQuery: DefaultTestQuery)
        {
        }
        public NpgsqlHealthCheck(string connectionString, string testQuery)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            TestQuery = testQuery;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);

                    if (TestQuery != null)
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = TestQuery;

                        await command.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
                catch(DbException ex) 
                {
                    return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
                }
            }

            return HealthCheckResult.Healthy($"Connection to {ConnectionString} established successfully.");
        }
    }
}