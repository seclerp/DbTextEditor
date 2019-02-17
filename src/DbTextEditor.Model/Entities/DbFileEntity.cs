namespace DbTextEditor.Model.Entities
{
    public class DbFileEntity
    {
        public string Name { get; set; }
        public int Revision { get; set; }
        public byte[] Contents { get; set; }
    }
}