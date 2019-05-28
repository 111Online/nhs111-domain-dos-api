using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PathwayService;

namespace NHS111.Domain.Dos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DosController : ControllerBase
    {
        private readonly IPathwayServiceSoapFactory _pathWayServiceFactory;

        public DosController(IPathwayServiceSoapFactory pathWayServiceFactory, IConfiguration configuration)
        {
            _pathWayServiceFactory = pathWayServiceFactory;
        }

        [HttpPost]
        [Route("DOSapi/CheckCapacitySummary")]
        public async Task<ActionResult<CheckCapacitySummaryResponse>> CheckCapacitySummary([FromBody]CheckCapacitySummaryRequest dosRequest, [FromQuery]string endpoint = "")
        {
            var client = _pathWayServiceFactory.Create(endpoint);
            return await client.CheckCapacitySummaryAsync(dosRequest);
        }

        [HttpPost]
        [Route("DOSapi/ServiceDetailsById")]
        public async Task<ActionResult<ServiceDetailsByIdResponse>> ServiceDetailsById([FromBody]ServiceDetailsByIdRequest dosRequest)
        {
            var client = _pathWayServiceFactory.Create();
            return await client.ServiceDetailsByIdAsync(dosRequest);
        }
    }
}