namespace Labo.Cms.Core.Models
{
    public sealed class Parameter
    {
        public string Name { get; set; }

        public DataType DataType { get; set; }

        public object Value { get; set; }
    }
}