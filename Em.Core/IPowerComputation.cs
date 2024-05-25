using System;
using System.Collections.Generic;
using System.Text;

namespace Em.Core
{
    public interface IPowerComputation
    {
        void Init(LoadInput powerInput);

        void Compute();

        List<DistributionOutput> DistributionOutputList();

        float Co2TotalEmission();

        double EnergyCost();
    }
}
