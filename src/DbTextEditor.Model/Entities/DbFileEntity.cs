namespace DbTextEditor.Model.Entities
{
    public struct DbFileEntity
    {
        // Name is unique key for repository
        public string Name { get; set; }
        public byte[] Contents { get; set; }
    }
}