using System;
using System.Collections.Generic;
using System.Text;

namespace Em.Core
{
    public class DistributionOutput
    {
        public DistributionOutput(string newName,int power)
        {
            name = newName;
            p = power;
        }
        public string name { get; set; }
        public int p { get; set; }
    }
}
