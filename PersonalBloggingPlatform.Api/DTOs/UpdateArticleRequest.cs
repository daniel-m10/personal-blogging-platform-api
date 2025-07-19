using System.ComponentModel.DataAnnotations;

namespace PersonalBloggingPlatform.Api.DTOs
{
    public class UpdateArticleRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public List<string> Tags { get; set; } = [];
    }
}
