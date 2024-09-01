using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practice_integration_api.Application;

namespace practice_integration_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly BlobIntegration _blobIntegration;

        public BlobController(BlobIntegration blobIntegration)
        {
            _blobIntegration = blobIntegration;
        }

        [HttpPost]
        public IActionResult ExecuteCopyBlob([FromBody] ExecuteCopyDTO dto)
        {
            _blobIntegration.CopyBlob(dto.SourceContainer, dto.SourceBlob, dto.DestinationContainer, dto.DestinationBlob);
            return Ok("Copying blob asynchronously...");
        }
    }
}
