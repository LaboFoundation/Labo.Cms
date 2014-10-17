namespace Labo.Cms.Core.Routing
{
    using System.Web.Routing;
    using System.Web.SessionState;

    public sealed class RouteInfo
    {
        public string Name { get; set; }

        public int Priority { get; set; }

        public RouteBase Route { get; set; }

        public SessionStateBehavior SessionState { get; set; }
    }
}