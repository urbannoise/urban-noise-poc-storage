using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using UrbanNoise.POC.Storage.Repositories.Abstractions;
using UrbanNoise.POC.Storage.Repositories.Contracts;

namespace UrbanNoise.POC.Storage.Repositories.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services, string table, string connectionString)
        {
            services.AddTransient<INoiseRepository, NoiseRepository>();
            services.AddTransient<IPartitionedRespository<NoiseRecordEntity>, TableStorageRepository>();
            services.Configure<RepositoryConfiguration>(conf =>
            {
                conf.TableName = table;
                conf.ConnectionString = connectionString;
            });
            return services;
        }
    }
}
