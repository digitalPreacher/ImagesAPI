using ImagesAPI.Models;

namespace ImagesAPI.Repository
{
    public interface IImagesRepostory
    {
        public Task<List<ImageModel>> GetImages();

        public Task<ImageModel> GetImage(int id);

        public bool UploadImage(IFormFile file);
    }
}
