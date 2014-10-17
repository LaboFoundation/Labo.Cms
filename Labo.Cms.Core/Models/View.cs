namespace Labo.Cms.Core.Models
{
    using System.Collections.Generic;

    public sealed class View
    {
        private List<Parameter> m_Parameters;

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Parameter> Parameters
        {
            get
            {
                return m_Parameters ?? (m_Parameters = new List<Parameter>());
            }

            set
            {
                m_Parameters = value;
            }
        }
    }
}
