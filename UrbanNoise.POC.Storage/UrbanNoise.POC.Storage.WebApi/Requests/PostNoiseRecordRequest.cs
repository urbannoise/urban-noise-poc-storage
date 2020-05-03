using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrbanNoise.POC.Storage.Domain.Abstractions;

namespace UrbanNoise.POC.Storage.WebApi.Requests
{
    public class PostNoiseRecordRequest
    {
        [FromBody()]
        public NoiseRecord Record { get; set; }
    }
}
