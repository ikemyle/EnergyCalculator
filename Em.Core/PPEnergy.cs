using System;
using System.Collections.Generic;
using System.Text;

namespace Em.Core
{
    /// <summary>
    /// Power plant energy stats
    /// </summary>
    public class PPEnergy
    {
        /// <summary>
        /// Compute price and CO2 emission
        /// </summary>
        private void CalculateCostAndCo2()
        {
            if (PowerPlantType == EnergyType.windturbine)
            {
                EnergyCost = 0;
            }
            else
            {
                EnergyCost = MaxProducedEnergy * EuroPerMWH;
            }
            if (Co2Euroton != 0)
            {
                Co2Emission = EnergyCost / Co2Euroton;
            }
        }

        /// <summary>
        /// inityialize the class
        /// </summary>
        /// <param name="enrgyType"></param>
        /// <param name="energyMWH"></param>
        /// <param name="cost"></param>
        public PPEnergy(string ppName, EnergyType energyType, int pMin, int pMax, float efficiency, float euroMWH, int co2Euroton,int merit)
        {
            PowerPlantName = ppName;
            PowerPlantType = energyType;
            MimumPower = pMin;
            EuroPerMWH = euroMWH;
            Co2Euroton = co2Euroton;
            Merit = merit;
            MaxProducedEnergy = Convert.ToInt32((efficiency * pMax) / 100);

            CalculateCostAndCo2();
        }

        public void ReAdjustMax(int MaxEnergyNeeded)
        {
            if (MaxEnergyNeeded > MaxProducedEnergy)
            {
                throw new Exception("This turbine cannot produce this amount of energy per hour");
            }
            MaxProducedEnergy = MaxEnergyNeeded;
            CalculateCostAndCo2();
        }
        /// <summary>
        /// Power plant type
        /// </summary>
        public EnergyType PowerPlantType { get; set; }
        /// <summary>
        /// Mimimum amount of power that a power plant must send if started
        /// </summary>
        public int MimumPower { get; set; }
        /// <summary>
        /// MWH of energy produced (needed)
        /// </summary>
        public int MaxProducedEnergy { get; set; }
        /// <summary>
        /// Name of the power plant
        /// </summary>
        public string PowerPlantName { get; set; }
        /// <summary>
        /// Cost of the energy
        /// </summary>
        public float EnergyCost { get; set; }
        /// <summary>
        /// The CO2 emission for the power plant to produce the amount of energy needed
        /// </summary>
        public float Co2Emission { get; set; }
        /// <summary>
        /// How much money is needed for a MWH
        /// </summary>
        public float EuroPerMWH { get; set; }
        /// <summary>
        /// Emission of CO2 per Euro spent
        /// </summary>
        public int Co2Euroton { get; set; }

        /// <summary>
        /// Property that shows how is the power plant position in the merit order
        /// </summary>
        public int Merit { get; set; }
    }
}
