using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Model
{
	public class NewsRequestModel
    { 
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = null!;

        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; } = null!;
        public string? Image { get; set; }
    }
}

