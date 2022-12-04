using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Persistence;

namespace API.Extensions
{
    public static class Postgres
    {
        public static IServiceCollection AddPostgress(this IServiceCollection services, IConfiguration config) 
        {
          services.AddDbContext<DataContext>(opt => 
          {

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if(env == "Development")
            {
              string connectionString = config.GetConnectionString("DefaultConnection");

              opt.UseNpgsql(connectionString);
            } 
            else
            {
              var connectionStringBuilder = new NpgsqlConnectionStringBuilder()
              {
                 // The Cloud SQL proxy provides encryption between the proxy and instance.
                SslMode = SslMode.Disable,

                // Note: Saving credentials in environment variables is convenient, but not
                // secure - consider a more secure solution such as
                // Cloud Secret Manager (https://cloud.google.com/secret-manager) to help
                // keep secrets safe.
                // Environment.GetEnvironmentVariable("INSTANCE_UNIX_SOCKET") ?? 

                // notes for deployment - remove when in PROD
                // DB_PASS = 12345
                // DB_NAME = shogun
                // DB_USER = postgres
                // DB_INSTANCE = adv-dev-dotnet:us-central1:shogun-postgres

                // name the db as shogun-postgres
                // password as 12345
                // DB user and name can be found DQL and in the database navigation

                // Make sure to add the cloud sql instance in the cloud run container - else you will get that it can't connect even if all env vars are passed

                Host = Environment.GetEnvironmentVariable("DB_INSTANCE"), // e.g. '/cloudsql/project:region:instance'
                Username = Environment.GetEnvironmentVariable("DB_USER"), // e.g. 'my-db-user
                Password = Environment.GetEnvironmentVariable("DB_PASS"), // e.g. 'my-db-password'
                Database = Environment.GetEnvironmentVariable("DB_NAME"), // e.g. 'my-database'

              };

              connectionStringBuilder.Pooling = true;

              connectionStringBuilder.MaxPoolSize = 5;
              connectionStringBuilder.MinPoolSize = 0;
              connectionStringBuilder.Timeout = 15;
              connectionStringBuilder.ConnectionIdleLifetime = 300;

              opt.UseNpgsql(connectionStringBuilder.ConnectionString);
            }
          });


          return services;
        }
    }
}