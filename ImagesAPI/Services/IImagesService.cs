using ImagesAPI.Models;

namespace ImagesAPI.Services
{
    public interface IImagesService
    {

        public Task<List<ImageModel>> GetImages();

        public Task<ImageModel> GetImage(int id);

        public bool UploadImage(IFormFile file);
    }
}
