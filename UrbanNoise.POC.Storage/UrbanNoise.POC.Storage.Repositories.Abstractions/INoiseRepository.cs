using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanNoise.POC.Storage.Domain.Abstractions;

namespace UrbanNoise.POC.Storage.Repositories.Abstractions
{
    public interface INoiseRepository
    {
        Task<IEnumerable<NoiseRecord>> Get(string city
            , string street
            , DateTime rangeTimeStart
            , DateTime rangeTimeEnd
            , int start = 0
            , int pageSize = int.MaxValue
            );
        Task<NoiseRecord> Save(NoiseRecord record);
        Task<NoiseRecord> Update(NoiseRecord record);
    }
}
