using ImagesAPI.Models;
using ImagesAPI.Repository;

namespace ImagesAPI.Services
{
    public class ImagesService : IImagesService
    {
        private readonly IImagesRepostory _imagesRepostory;

        public ImagesService(IImagesRepostory imagesRepostory)
        {
            _imagesRepostory = imagesRepostory;
        }

        public async Task<List<ImageModel>> GetImages()
        {
            return await _imagesRepostory.GetImages();
        }

        public async Task<ImageModel> GetImage(int id)
        {
            return await _imagesRepostory.GetImage(id);
        }

        public bool UploadImage(IFormFile file)
        {
           return _imagesRepostory.UploadImage(file);
        }
    }
}
