namespace Labo.Cms.Core.Tests.Routing
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;

    using Labo.Cms.Core.Routing;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class DefaultRouteManagerFixture
    {
        [Test]
        public void RegisterRoute()
        {
            IPageContextScopeManager pageContextScopeManager = Substitute.For<IPageContextScopeManager>();
            DefaultRouteManager defaultRouteManager = new DefaultRouteManager(new RouteCollection(), pageContextScopeManager);

            RouteInfo routeInfo = new RouteInfo();
            defaultRouteManager.RegisterRoute(routeInfo);

            Assert.AreEqual(1, defaultRouteManager.RouteInfos.Count);
            Assert.AreEqual(routeInfo, defaultRouteManager.RouteInfos[0]);
        }

        [Test]
        public void InstallRoutes()
        {
            IPageContextScopeManager pageContextScopeManager = Substitute.For<IPageContextScopeManager>();
            RouteCollection routeCollection = new RouteCollection();
            DefaultRouteManager defaultRouteManager = new DefaultRouteManager(routeCollection, pageContextScopeManager);

            Route route1 = new Route("test", new MvcRouteHandler());
            Route route2 = new Route("test", new MvcRouteHandler());
           
            RouteInfo route1Info = new RouteInfo { Name = "route1", Route = route1, Priority = 1, SessionState = SessionStateBehavior.Disabled };
            RouteInfo route2Info = new RouteInfo { Name = "route2", Route = route2, Priority = 2, SessionState = SessionStateBehavior.ReadOnly };
           
            defaultRouteManager.RegisterRoute(route1Info);
            defaultRouteManager.RegisterRoute(route2Info);

            defaultRouteManager.InstallRoutes();

            Assert.AreEqual(2, routeCollection.Count);

            LaboCmsRoute route = (LaboCmsRoute)routeCollection[0];
            Assert.AreEqual(route1, route.Route);
            Assert.AreEqual(pageContextScopeManager, route.PageContextScopeManager);
            Assert.AreEqual(SessionStateBehavior.Disabled, route.SessionStateBehavior);

            route = (LaboCmsRoute)routeCollection[1];
            Assert.AreEqual(route2, route.Route);
            Assert.AreEqual(pageContextScopeManager, route.PageContextScopeManager);
            Assert.AreEqual(SessionStateBehavior.ReadOnly, route.SessionStateBehavior);
        }
    }
}
