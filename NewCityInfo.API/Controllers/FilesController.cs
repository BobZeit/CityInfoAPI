using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace NewCityInfo.API.Controllers
{
    [Route("api/files")]
    [Authorize]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _provider;

        public FilesController(FileExtensionContentTypeProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
        [HttpGet("{fileId}")]
        public ActionResult GetFiles(string fileId)
        {
            var filePath = "getting-started-with-rest-slides.pdf";
            if(!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            var fileByte = System.IO.File.ReadAllBytes(filePath);
            _provider.TryGetContentType(filePath, out var contentType);
            if (contentType == null)
            {
                contentType = "application/octet-stream";
            }
            return File(fileByte, contentType, Path.GetFileName(filePath));
        }
    }
}
