namespace Labo.Cms.Core.Models
{
    public sealed class Page
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public Layout Layout { get; set; }
    }
}