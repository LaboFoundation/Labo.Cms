namespace Labo.Cms.Core.Tests.Routing
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;

    using Labo.Cms.Core.Routing;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class LaboCmsRouteFixture
    {
        [Test]
        public void GetRouteData()
        {
            IPageContextScopeManager pageContextScopeManager = Substitute.For<IPageContextScopeManager>();
            SessionStateBehavior sessionStateBehaviorResult = SessionStateBehavior.Default;
            
            HttpContextBase httpContextBase = Substitute.For<HttpContextBase>();
            httpContextBase.When(x => x.SetSessionStateBehavior(Arg.Any<SessionStateBehavior>()))
                .Do(x => sessionStateBehaviorResult = x.Arg<SessionStateBehavior>());

            MvcRouteHandler mvcRouteHandler = new MvcRouteHandler();
            Route route = Substitute.For<Route>(
                "{*PageUrl}",
                new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } },
                mvcRouteHandler);

            RouteData originalRouteData = new RouteData { RouteHandler = mvcRouteHandler };            
            route.GetRouteData(Arg.Any<HttpContextBase>()).ReturnsForAnyArgs(x => originalRouteData);

            const SessionStateBehavior sessionStateBehavior = SessionStateBehavior.ReadOnly;
            LaboCmsRoute laboCmsRoute = new LaboCmsRoute(route, pageContextScopeManager, sessionStateBehavior);
            RouteData routeData = laboCmsRoute.GetRouteData(httpContextBase);

            Assert.IsNotNull(routeData);
            Assert.AreEqual(sessionStateBehavior, sessionStateBehaviorResult);
            Assert.AreEqual(originalRouteData, routeData);
            Assert.IsAssignableFrom(typeof(LaboCmsRouteHandler), routeData.RouteHandler);

            LaboCmsRouteHandler laboCmsRouteHandler = (LaboCmsRouteHandler)routeData.RouteHandler;
            Assert.AreEqual(sessionStateBehavior, laboCmsRouteHandler.SessionStateBehavior);
            Assert.AreEqual(mvcRouteHandler, laboCmsRouteHandler.RouteHandler);
            Assert.AreEqual(pageContextScopeManager, laboCmsRouteHandler.PageContextScopeManager);
        }
    }
}
