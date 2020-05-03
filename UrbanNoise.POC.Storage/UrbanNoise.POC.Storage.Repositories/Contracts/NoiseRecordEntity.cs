using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos.Table;

namespace UrbanNoise.POC.Storage.Repositories.Contracts
{
    internal class NoiseRecordEntity : TableEntity
    {
        /// <summary>
        /// Name of the city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Name of the street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Number along the street
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Moment when the noise recording started
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Contains the whole data of the Noise record
        /// </summary>
        public string Data { get; set; }

        public static string GetPartitionKey(string city, string street) => $"{city}--{street}";
        public static string GetRowKey(int number, DateTime startTime) => $"{number}--{startTime:yyyy-MM-dd HH:mm:ss:fff}";
    }
}
