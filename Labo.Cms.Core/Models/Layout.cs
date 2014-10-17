namespace Labo.Cms.Core.Models
{
    using System.Collections.Generic;

    public sealed class Layout
    {
        public int Id { get; set; }

        public string Name { get; set; }

        private IList<Pane> m_Panes;

        public IList<Pane> Panes
        {
            get
            {
                return m_Panes ?? (m_Panes = new List<Pane>());
            }

            set
            {
                m_Panes = value;
            }
        }
    }
}