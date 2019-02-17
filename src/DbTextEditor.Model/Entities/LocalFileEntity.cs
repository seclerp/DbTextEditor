namespace DbTextEditor.Model.Entities
{
    public struct LocalFileEntity
    {
        // Path is unique key for repository
        public string Path { get; set; }
        public string Contents { get; set; }
    }
}