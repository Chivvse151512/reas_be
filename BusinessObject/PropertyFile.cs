namespace BusinessObject
{
    public partial class PropertyFile
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string File { get; set; } = null!;
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Property Property { get; set; } = null!;
    }
}
