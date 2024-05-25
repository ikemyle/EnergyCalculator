using System;
using System.Text.Json.Serialization;

namespace Em.Core
{
    /// <summary>
    /// The object that will be passed to the service in order to compute the energy needed from each plant
    /// </summary>
    public class LoadInput
    {
        /// <summary>
        /// Power load energy needed
        /// </summary>
        public int load { get; set; }
        /// <summary>
        /// Energy prices and conditions
        /// </summary>
        public Fuels fuels { get; set; }
        /// <summary>
        /// Power plant information and effciency
        /// </summary>
        public Powerplant[] powerplants { get; set; }
    }

    /// <summary>
    /// Type of fuels
    /// </summary>
    public class Fuels
    {
        /// <summary>
        /// gas price per MWH
        /// </summary>
        [JsonPropertyName("gas(euro/MWh)")]
        public float gaseuroMWh { get; set; }
        /// <summary>
        /// kerosine price per MWH
        /// </summary>
        [JsonPropertyName("kerosine(euro/MWh)")]
        public float kerosineeuroMWh { get; set; }
        /// <summary>
        /// co2 emission per ton
        /// </summary>
        [JsonPropertyName("co2(euro/ton)")]
        public int co2euroton { get; set; }
        /// <summary>
        /// wind guts percentage
        /// </summary>
        [JsonPropertyName("wind(%)")]
        public int wind { get; set; }
    }

    /// <summary>
    /// Power plant definition
    /// </summary>
    public class Powerplant
    {
        /// <summary>
        /// Name of the power plant
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Energy type of the power plant
        /// </summary>
        public string type { get; set; }
        public float efficiency { get; set; }
        /// <summary>
        /// Mimimum produced power
        /// </summary>
        public int pmin { get; set; }
        /// <summary>
        /// Maximum produced power
        /// </summary>
        public int pmax { get; set; }
    }

}