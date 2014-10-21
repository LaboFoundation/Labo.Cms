namespace Labo.Cms.Core.Services
{
    using System.Collections.Generic;

    using Labo.Cms.Core.Models;

    public sealed class CmsService : ICmsService
    {
        public Page GetPageBySlug(string slug)
        {
            return new Page
                       {
                           Name = "Test",
                           Layout =
                               new Layout
                                   {
                                       Name = "TwoColumns",
                                       Panes =
                                           new List<Pane>
                                               {
                                                   new Pane 
                                                   {
                                                       Name = "LeftColumn", 
                                                       Containers = new List<Container>
                                                                        {
                                                                            new Container
                                                                                {
                                                                                    Name = "Simple",
                                                                                    View = new View
                                                                                               {
                                                                                                   Name = "View"
                                                                                               }
                                                                                }
                                                                        }
                                                   },
                                                   new Pane
                                                       {
                                                           Name = "RightColumn", 
                                                           Containers = new List<Container>
                                                                            {
                                                                                new Container
                                                                                    {
                                                                                        Name = "Simple",
                                                                                        View = new View
                                                                                               {
                                                                                                   Name = "View"
                                                                                               }
                                                                                    }
                                                                            }
                                                       }
                                               }
                                   }
                       };
        }
    }
}
