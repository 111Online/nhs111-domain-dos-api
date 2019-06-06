using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHS111.Domain.Dos.Api.Services;

namespace NHS111.Domain.Dos.Api.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorController : ControllerBase
    {
        private readonly IMonitorService _monitor;

        public MonitorController(IMonitorService monitor)
        {
            _monitor = monitor;
        }

        [HttpGet]
        [Route("{service}")]
        public async Task<string> MonitorPing(string service)
        {
            switch (service.ToLower())
            {
                case "ping":
                    return _monitor.Ping();

                case "metrics":
                    return _monitor.Metrics();

                case "health":
                    return (await _monitor.Health()).ToString();

                case "version":
                    return _monitor.Version();
            }

            return null;
        }
    }
}