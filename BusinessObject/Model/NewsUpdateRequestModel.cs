using System;
namespace BusinessObject.Model
{
	public class NewsUpdateRequestModel
	{
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
    }
}

