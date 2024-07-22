using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        //POST: api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imgUploadDto)
        {
            ValidateFileUpload(imgUploadDto);

            if (ModelState.IsValid)
            {
                //Convert DTO to Domain Model
                var imageDomainModel = new Image
                {
                    File = imgUploadDto.File,
                    FileName = imgUploadDto.FileName,
                    FileDescription = imgUploadDto.FileDescription,
                    FileExtension = Path.GetExtension(imgUploadDto.File.FileName),
                    FileSizeInBytes = imgUploadDto.File.Length
                };

                //User repository Upload image
                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }


        private void ValidateFileUpload(ImageUploadRequestDto imgUploadDto)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imgUploadDto.File.FileName)))
            {
                ModelState.AddModelError("File", "Invalid file extension. Only .jpg, .jpeg, .png files are allowed.");
            }

            if (imgUploadDto.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size exceeds 10MB.");
            }
        }
    }
}
