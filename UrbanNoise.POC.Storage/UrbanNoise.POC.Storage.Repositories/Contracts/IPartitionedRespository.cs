using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanNoise.POC.Storage.Domain.Abstractions;

namespace UrbanNoise.POC.Storage.Repositories.Contracts
{
    internal interface IPartitionedRespository<TEntity>
    {
        Task<TEntity> Get(string partitionKey, string rowKey);
        Task<IEnumerable<TEntity>> Query(Func<TEntity, bool> filter, int start, int pageSize);
        Task SaveOrUpdate(TEntity entity);
    }
}
