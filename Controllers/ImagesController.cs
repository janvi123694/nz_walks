using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nz_walks.Models.DTO;
using nz_walks.Repositories;
using NZWalks.Models.Domain;

namespace nz_walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository; 
        
        public ImagesController(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository; 
        }
        
        // /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };

                await _imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);

            }

            return BadRequest(ModelState); 
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File extension");
            }

            if (request.File.Length > 1048760)
            {
                ModelState.AddModelError("file", "file size greater than 10 mb");
            }
            
        }
    }
}
