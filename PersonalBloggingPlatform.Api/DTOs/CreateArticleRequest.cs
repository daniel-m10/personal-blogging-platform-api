using System.ComponentModel.DataAnnotations;

namespace PersonalBloggingPlatform.Api.DTOs
{
    public class CreateArticleRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public List<string> Tags { get; set; } = [];

        [Required]
        public string Author { get; set; } = string.Empty;
    }
}
