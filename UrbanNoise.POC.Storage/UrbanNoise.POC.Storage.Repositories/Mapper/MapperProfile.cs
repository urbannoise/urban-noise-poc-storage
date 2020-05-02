using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using UrbanNoise.POC.Storage.Domain.Abstractions;
using UrbanNoise.POC.Storage.Repositories.Contracts;

namespace UrbanNoise.POC.Storage.Repositories.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<NoiseRecord, NoiseRecordEntity>()
                .ForMember(dest => dest.PartitionKey, conf => conf.MapFrom(src => NoiseRecordEntity.GetPartitionKey(src.City, src.Street)))
                .ForMember(dest => dest.RowKey, conf => conf.MapFrom(src => NoiseRecordEntity.GetRowKey(src.Number, src.StartTime)))
                .ForMember(dest => dest.Data, conf => conf.MapFrom(src => JsonConvert.SerializeObject(src)))
                ;
            CreateMap<NoiseRecordEntity, NoiseRecord>()
                .AfterMap((src, dest) =>
                {
                    NoiseRecord data = JsonConvert.DeserializeObject<NoiseRecord>(src.Data);
                    dest.Instants = data.Instants;
                    dest.Intensities = data.Intensities;
                })
                ;
        }
    }
}
