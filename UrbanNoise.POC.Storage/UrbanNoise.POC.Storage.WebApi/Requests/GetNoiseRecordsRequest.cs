using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UrbanNoise.POC.Storage.WebApi.Requests
{
    public class GetNoiseRecordsRequest
    {
        [FromQuery(Name = nameof(City))]
        public string City { get; set; }

        [FromQuery(Name = nameof(Street))]
        public string Street { get; set; }

        [FromQuery(Name = nameof(Number))]
        public int Number { get; set; }

        [FromQuery(Name = nameof(RangeTimeStart))]
        public DateTime RangeTimeStart { get; set; }

        [FromQuery(Name = nameof(RangeTimeEnd))]
        public DateTime RangeTimeEnd { get; set; }

        [FromQuery(Name = nameof(PageIndex))]
        public int PageIndex { get; set; } = 0;

        [FromQuery(Name = nameof(PageSize))]
        public int PageSize { get; set; } = int.MaxValue;
    }
}
