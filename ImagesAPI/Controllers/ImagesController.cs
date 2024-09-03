using ImagesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImagesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesService _imageService;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(IImagesService imageService, ILogger<ImagesController> logger)
        {
            _imageService = imageService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            var images = await _imageService.GetImages();
            if(images.Count == 0) 
            {
                return NotFound(); 
            }

            try
            {
                var imagesId = images.Select(x => x.Id).ToList();

                var random = new Random();
                var randomIndex = random.Next(0, images.Count);

                var image = await _imageService.GetImage(imagesId[randomIndex]);
                var imagePath = image.Path;

                var byteImage = System.IO.File.ReadAllBytes(imagePath);

                return File(byteImage, "image/jpeg");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile file)
        {

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension)) 
            {
                return BadRequest("Invalid file type");
            }

            try
            {
                var result = _imageService.UploadImage(file);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Image already exist");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

                return BadRequest();
            }
            
        }

    }
}
