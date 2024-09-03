using System.ComponentModel.DataAnnotations;

namespace ImagesAPI.Models
{
    public class ImageModel
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; } = string.Empty;
    }
}
