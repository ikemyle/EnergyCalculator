using System;
using System.Collections.Generic;
using System.Globalization;
using Em.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CoreApp.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerController : ControllerBase
    {
        private IMemoryCache _cache;
        private IPowerComputation _compute;
        private readonly ILogger<PowerController> logger;

        public PowerController(IMemoryCache memoryCache, IPowerComputation compute,
                         ILogger<PowerController> logger)
        {
            this._cache = memoryCache;
            this._compute = compute;
            this.logger = logger;
        }

        // POST: api/Power
        [HttpPost]
        [Produces("application/json")]
        public IEnumerable<DistributionOutput> Post([FromBody] LoadInput LoadInput)
        {
            try
            {
                logger.LogInformation("Post execution started at " + DateTime.Now.ToString());
                _compute.Init(LoadInput);
                _compute.Compute();
                _cache.Set("CO2", _compute.Co2TotalEmission().ToString("n2", CultureInfo.InvariantCulture) + " ton");
                var output = _compute.DistributionOutputList();
                logger.LogInformation("Post execution completed at " + DateTime.Now.ToString());
                return output;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        // Get: api/Power
        [HttpGet]
        public string Get()
        {
            object co2News;
            _cache.TryGetValue("CO2", out co2News);
            if (co2News != null)
            {
                return co2News.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
