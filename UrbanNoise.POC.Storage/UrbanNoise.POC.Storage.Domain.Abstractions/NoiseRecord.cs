using System;

namespace UrbanNoise.POC.Storage.Domain.Abstractions
{
    public class NoiseRecord
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
        /// Ellapsed seconds with respect the StartTime
        /// </summary>
        public double[] Instants { get; set; }

        /// <summary>
        /// Intensities at the specific Instants seconds
        /// </summary>
        public double[] Intensities { get; set; }
    }
}
