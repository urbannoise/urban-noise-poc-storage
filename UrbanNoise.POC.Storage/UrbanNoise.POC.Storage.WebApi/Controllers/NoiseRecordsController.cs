using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrbanNoise.POC.Storage.Domain.Abstractions;
using UrbanNoise.POC.Storage.Repositories.Abstractions;
using UrbanNoise.POC.Storage.WebApi.Requests;

namespace UrbanNoise.POC.Storage.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoiseRecordsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<NoiseRecordsController> _logger;
        private readonly INoiseRepository _repository;

        public NoiseRecordsController(
            ILogger<NoiseRecordsController> logger
            , INoiseRepository repository)
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetNoiseRecordsRequest request)
        {
            _logger.LogInformation($"{nameof(GetNoiseRecordsRequest)} received");

            IEnumerable<NoiseRecord> result = await _repository.Get(
                request.City
                , request.Street
                , request.RangeTimeStart
                , request.RangeTimeEnd
                , request.PageIndex
                , request.PageSize
                ).ConfigureAwait(false);
            IActionResult response = result.Any()
                ? Ok(result)
                : (IActionResult)NotFound();

            _logger.LogInformation($"{nameof(GetNoiseRecordsRequest)} processed");

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] PostNoiseRecordRequest request)
        {
            _logger.LogInformation($"{nameof(PostNoiseRecordRequest)} received");

            NoiseRecord savedRecord = await _repository.Save(request.Record).ConfigureAwait(false);
            IActionResult response = Created("", savedRecord);

            _logger.LogInformation($"{nameof(PostNoiseRecordRequest)} processed");

            return response;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] PutNoiseRecordRequest request)
        {
            _logger.LogInformation($"{nameof(PutNoiseRecordRequest)} received");

            NoiseRecord savedRecord = await _repository.Update(request.Record).ConfigureAwait(false);
            IActionResult response = Ok(savedRecord);

            _logger.LogInformation($"{nameof(PutNoiseRecordRequest)} processed");

            return response;
        }
    }
}
