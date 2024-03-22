namespace BusinessObject
{
    public partial class News
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? Image { get; set; }
    }
}
