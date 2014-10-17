namespace Labo.Cms.Core.Models
{
    using System.Collections.Generic;

    public sealed class Site
    {
        private IList<Page> m_Screens;

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Page> Screens
        {
            get
            {
                return m_Screens ?? (m_Screens = new List<Page>());
            }

            set
            {
                m_Screens = value;
            }
        }
    }
}