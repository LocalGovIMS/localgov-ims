namespace Admin.Models.Shared
{
    public class MetadataViewModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string ParentCode { get; set; }
    }
}