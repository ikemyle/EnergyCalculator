using System;
using System.Collections.Generic;
using System.Text;

namespace Em.Core
{
    public class MeritOrders
    {
        public MeritOrders()
        {
            MeritOrdersList = new List<MeritOrder>();
            MeritOrdersList.AddRange(new List<MeritOrder>
            {
                new MeritOrder(EnergyType.windturbine, 1),
                new MeritOrder(EnergyType.gasfired, 2),
                new MeritOrder(EnergyType.turbojet, 3),
            });
        }

        public List<MeritOrder> MeritOrdersList { get; set; }
    }
}
