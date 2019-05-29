using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NHS111.Domain.Dos.Api.Models.Request;
using PathwayService;

namespace NHS111.Domain.Dos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DosController : ControllerBase
    {
        private readonly IPathwayServiceSoapFactory _pathWayServiceFactory;
        private readonly IMapper _mapper;

        public DosController(IPathwayServiceSoapFactory pathWayServiceFactory, IConfiguration configuration, IMapper mapper)
        {
            _pathWayServiceFactory = pathWayServiceFactory;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("CheckCapacitySummary")]
        public async Task<ActionResult<CheckCapacitySummaryResponse>> CheckCapacitySummary([FromBody]DosCheckCapacitySummaryRequest dosRequest, [FromQuery]string endpoint = "")
        {
            var client = _pathWayServiceFactory.Create(endpoint);
            var checkCapacitysummaryRequest = _mapper.Map<CheckCapacitySummaryRequest>(dosRequest);
            return await client.CheckCapacitySummaryAsync(checkCapacitysummaryRequest);
        }

        [HttpPost]
        [Route("ServiceDetailsById")]
        public async Task<ActionResult<ServiceDetailsByIdResponse>> ServiceDetailsById([FromBody]DosServiceDetailsByIdRequest dosRequest)
        {
            var client = _pathWayServiceFactory.Create();
            var serviceDetailsByIdRequest = _mapper.Map<ServiceDetailsByIdRequest>(dosRequest);
            return await client.ServiceDetailsByIdAsync(serviceDetailsByIdRequest);
        }
    }
}