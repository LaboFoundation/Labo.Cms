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
                                                       Views = new List<View>
                                                                        {
                                                                            new View
                                                                                {
                                                                                    Name = "View",
                                                                                    Container = new Container
                                                                                               {
                                                                                                   Name = "Simple"
                                                                                               }
                                                                                },
                                                                                new View
                                                                                    {
                                                                                        Name = "Labo.Cms.Modules.Root.Views.ContactUs.Index",
                                                                                        Container = new Container
                                                                                                        {
                                                                                                            Name = "Simple"
                                                                                                        }
                                                                                    }
                                                                        }
                                                   },
                                                   new Pane
                                                       {
                                                           Name = "RightColumn", 
                                                           Views = new List<View>
                                                                        {
                                                                            new View
                                                                                {
                                                                                    Name = "View",
                                                                                    Container = new Container
                                                                                               {
                                                                                                   Name = "Simple"
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
