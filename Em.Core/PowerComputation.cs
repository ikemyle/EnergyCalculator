using System;
using System.Collections.Generic;
using System.Linq;

namespace Em.Core
{
    /// <summary>
    /// Power Computation class that generate the wanted output
    /// </summary>
    public class PowerComputation: IPowerComputation
    {
        /// <summary>
        /// Load input object used for computation. Retrieved in Init method
        /// </summary>
        private LoadInput LoadInput;

        /// <summary>
        /// empty constructor needed for dependency injection
        /// </summary>
        public PowerComputation()
        {
        }

        /// <summary>
        /// Initialization of the computation class by loading the input
        /// </summary>
        /// <param name="powerInput"></param>
        public void Init(LoadInput powerInput)
        {
            if (powerInput == null)
            {
                throw new Exception("Invalid load input");
            }
            LoadInput = powerInput;
            NeededPowerPlants = new List<PPEnergy>();
        }

        /// <summary>
        /// Main computation function to create the output list
        /// </summary>
        public void Compute()
        {
            var loadNeeded = LoadInput.load;

            //look up the amount of produced energy by merit order
            var listOrder = (new MeritOrders()).MeritOrdersList.OrderBy(x => x.ETypeOrder);
            foreach (var itmOrder in listOrder)
            {
                var turbines = LoadInput.powerplants.Where(x => x.type == itmOrder.EType.ToString()).ToList();
                foreach (var turbine in turbines)
                {
                    if (loadNeeded == 0)  //all energy has been produced
                    {
                        //maintain only the list of unused turbines
                        NeededPowerPlants.Add(new PPEnergy(turbine.name, itmOrder.EType, turbine.pmin, turbine.pmax, 0, 0, 0, itmOrder.ETypeOrder));
                        continue;
                    }
                    PPEnergy ppEnergy;
                    float efficiencyCoeficient;
                    if (itmOrder.EType == EnergyType.windturbine)
                    {
                        efficiencyCoeficient = LoadInput.fuels.wind;
                    }
                    else
                    {
                        efficiencyCoeficient = turbine.efficiency * 100;
                    }

                    ppEnergy = ProducedEnergy(turbine.name, itmOrder.EType, efficiencyCoeficient, turbine.pmin, turbine.pmax, itmOrder.ETypeOrder);

                    if (ppEnergy == null)
                    {
                        continue;
                    }

                    //if we need less that the max energy that can be produced, we reduce it to the needed one only.
                    if (ppEnergy.MaxProducedEnergy > loadNeeded)
                    {
                        ppEnergy.ReAdjustMax(loadNeeded);
                    }

                    NeededPowerPlants.Add(ppEnergy);

                    //subtract to adjust the energy needed
                    loadNeeded -= ppEnergy.MaxProducedEnergy;
                }
            }
        }

        /// <summary>
        /// Ordered distribution list to be returned
        /// </summary>
        /// <returns></returns>
        public List<DistributionOutput> DistributionOutputList()
        {
            DistributionList = new List<DistributionOutput>();
            foreach (var ppEnergy in NeededPowerPlants.OrderBy(x => x.Merit).ThenByDescending(x => x.MaxProducedEnergy).ToList())
            {
                var outputItem = new DistributionOutput(ppEnergy.PowerPlantName, ppEnergy.MaxProducedEnergy);
                DistributionList.Add(outputItem);
            }
            return DistributionList;
        }

        /// <summary>
        /// Total CO2 emission for a specific load
        /// </summary>
        /// <returns></returns>
        public float Co2TotalEmission()
        {
            float totalCo2 = NeededPowerPlants.Sum(item => item.Co2Emission);
            return totalCo2;
        }

        /// <summary>
        /// Total energy cost for the load
        /// </summary>
        /// <returns></returns>
        public double EnergyCost()
        {
            double totalCost = NeededPowerPlants.Sum(item => item.EnergyCost);
            return totalCost;
        }

        #region Private
        /// <summary>
        /// List of needed power plants to produce the load
        /// </summary>
        private List<PPEnergy> NeededPowerPlants;

        /// <summary>
        /// The computed output based on load, merit and available power plants
        /// </summary>
        private List<DistributionOutput> DistributionList { get; set; }

        /// <summary>
        /// The information of the produced energy for any power plant based on input info
        /// </summary>
        /// <param name="name"></param>
        /// <param name="energyType"></param>
        /// <param name="efficiency"></param>
        /// <param name="pMin"></param>
        /// <param name="pMax"></param>
        /// <returns></returns>
        private PPEnergy ProducedEnergy(string name, EnergyType energyType, float efficiency, int pMin, int pMax, int merit)
        {
            float euroMWH = 0;
            if (energyType == EnergyType.gasfired)
            {
                euroMWH = LoadInput.fuels.gaseuroMWh;
            }
            else if (energyType == EnergyType.turbojet)
            {
                euroMWH = LoadInput.fuels.kerosineeuroMWh;
            }
            var ppEnergy = new PPEnergy(name, energyType, pMin, pMax, efficiency, euroMWH, LoadInput.fuels.co2euroton, merit);
            return ppEnergy;
        }

        #endregion
    }
}
