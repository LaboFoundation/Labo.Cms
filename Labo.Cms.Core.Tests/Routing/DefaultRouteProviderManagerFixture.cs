namespace Labo.Cms.Core.Tests.Routing
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;

    using Labo.Cms.Core.Routing;

    using NUnit.Framework;

    [TestFixture]
    public class DefaultRouteProviderManagerFixture
    {
        private class TestRouteProvider : IRouteProvider
        {
            public void RegisterRoutes(IRouteRegistrar routeRegistrar)
            {
                routeRegistrar.RegisterRoute(new RouteInfo
                                               {
                                                   Name = "test",
                                                   Priority = 1,
                                                   Route = new Route("test", new MvcRouteHandler()),
                                                   SessionState = SessionStateBehavior.Default
                                               });
            }
        }

        [Test]
        public void RegisterRouteProvider()
        {
            DefaultRouteProviderManager routeProviderManager = new DefaultRouteProviderManager();
            routeProviderManager.RegisterRouteProvider(new TestRouteProvider());
        }
    }
}
