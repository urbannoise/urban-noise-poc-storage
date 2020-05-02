using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UrbanNoise.POC.Storage.Domain.Abstractions;
using UrbanNoise.POC.Storage.Repositories.Abstractions;
using UrbanNoise.POC.Storage.Repositories.Contracts;

namespace UrbanNoise.POC.Storage.Repositories
{
    internal class NoiseRepository : INoiseRepository
    {
        private readonly IPartitionedRespository<NoiseRecordEntity> _repository;
        private readonly IMapper _mapper;

        public NoiseRepository(
            IPartitionedRespository<NoiseRecordEntity> repository
            , IMapper mapper
            )
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<NoiseRecord>> Get(
              string city
            , string street
            , DateTime rangeTimeStart
            , DateTime rangeTimeEnd
            , int start = 0
            , int pageSize = int.MaxValue
            )
        {
            bool Filters(NoiseRecordEntity record)
                => record.PartitionKey == $"{city}--{street}"
                    && rangeTimeStart <= record.StartTime
                    && record.StartTime <= rangeTimeEnd
                    ;
            IEnumerable<NoiseRecordEntity> records = await _repository.Query(Filters, start, pageSize).ConfigureAwait(false);

            return records
                .Select(record => _mapper.Map<NoiseRecord>(record));
        }

        public Task<NoiseRecord> Save(NoiseRecord record) => SaveOrUpdate(record);

        public Task<NoiseRecord> Update(NoiseRecord record) => SaveOrUpdate(record);

        private async Task<NoiseRecord> SaveOrUpdate(NoiseRecord record)
        {
            NoiseRecordEntity entity = _mapper.Map<NoiseRecordEntity>(record);
            await _repository.SaveOrUpdate(entity).ConfigureAwait(false);
            return record;
        }
    }
}
