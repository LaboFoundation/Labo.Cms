namespace Labo.Cms.Core.Models
{
    using System.Collections.Generic;

    public sealed class Pane
    {
        public int Id { get; set; }

        public string Name { get; set; }

        private IList<Container> m_Containers;

        public IList<Container> Containers
        {
            get
            {
                return m_Containers ?? (m_Containers = new List<Container>());
            }

            set
            {
                m_Containers = value;
            }
        }
    }
}