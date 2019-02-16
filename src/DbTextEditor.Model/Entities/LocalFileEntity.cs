namespace DbTextEditor.Model.Entities
{
    public struct LocalFileEntity
    {
        // Unique key for repository
        public string Path { get; set; }
        public string Contents { get; set; }
    }
}