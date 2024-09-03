using ImagesAPI.Data;
using ImagesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ImagesAPI.Repository
{
    public class ImagesRepository : IImagesRepostory
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ImagesRepository> _logger;

        public ImagesRepository(ApplicationDbContext context, 
            ILogger<ImagesRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ImageModel>> GetImages()
        {
            return await _context.Images.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<ImageModel> GetImage(int id)
        {
            return await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool UploadImage(IFormFile file)
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "Images",
                file.FileName);

            if (!File.Exists(path))
            {  
                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var image = new ImageModel { Path = path };

                    _context.Images.Add(image);
                    _context.SaveChanges();

                    return true;
                
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);

                    return false;
                }
            } 
            else
            {
                return false;
            }
            
        }
    }
}
