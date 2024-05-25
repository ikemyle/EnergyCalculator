using System;
using System.Collections.Generic;
using System.Text;

namespace Em.Core
{
    /// <summary>
    /// type of produced energy
    /// </summary>
    public enum EnergyType
    {
        gasfired,
        turbojet,
        windturbine
    }

    /// <summary>
    /// Merit order class
    /// </summary>
    public class MeritOrder
    {
        /// <summary>
        /// initialize the merit order
        /// </summary>
        /// <param name="newEtype"></param>
        /// <param name="newOrder"></param>
        public MeritOrder(EnergyType newEtype, int newOrder)
        {
            EType = newEtype;
            ETypeOrder = newOrder;
        }
        /// <summary>
        /// energy type of the order to be set
        /// </summary>
        public EnergyType EType { get; set; }
        /// <summary>
        /// order of the energy type
        /// </summary>
        public int ETypeOrder { get; set; }
    }
}
