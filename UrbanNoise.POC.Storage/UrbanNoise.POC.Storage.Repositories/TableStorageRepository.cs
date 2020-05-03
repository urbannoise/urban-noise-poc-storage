using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TableStorage.Abstractions.Store;
using UrbanNoise.POC.Storage.Repositories.Configuration;
using UrbanNoise.POC.Storage.Repositories.Contracts;

namespace UrbanNoise.POC.Storage.Repositories
{
    internal class TableStorageRepository : IPartitionedRespository<NoiseRecordEntity>
    {
        TableStore<NoiseRecordEntity> _tableStorage;

        public TableStorageRepository(IOptions<RepositoryConfiguration> configuration)
        {
            RepositoryConfiguration conf = configuration?.Value ?? throw new ArgumentNullException(nameof(configuration));
            TableStorageOptions options = new TableStorageOptions()
            {
                EnsureTableExists = true,
                Retries = 2,
                RetryWaitTimeInSeconds = 1,
            };
            _tableStorage = new TableStore<NoiseRecordEntity>(conf.TableName, conf.ConnectionString, options);
        }

        public async Task<NoiseRecordEntity> Get(string partitionKey, string rowKey)
            => await _tableStorage.GetRecordAsync(partitionKey, rowKey).ConfigureAwait(false);

        public async Task<IEnumerable<NoiseRecordEntity>> Query(Func<NoiseRecordEntity, bool> filter, int start, int pageSize)
            => await _tableStorage.GetRecordsByFilterAsync(filter, start, pageSize).ConfigureAwait(false);

        public async Task SaveOrUpdate(NoiseRecordEntity entity)
            => await _tableStorage.InsertOrReplaceAsync(entity).ConfigureAwait(false);
    }
}
