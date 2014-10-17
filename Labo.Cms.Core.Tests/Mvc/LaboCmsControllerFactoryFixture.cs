namespace Labo.Cms.Core.Tests.Mvc
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Labo.Cms.Core.Mvc;
    using Labo.Common.Ioc;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class LaboCmsControllerFactoryFixture
    {
        private class Test1Controller : IController, IDisposable
        {
            private bool m_Disposed = false;

            public bool Disposed
            {
                get
                {
                    return m_Disposed;
                }
            }

            public void Execute(RequestContext requestContext)
            {
            }

            public void Dispose()
            {
                m_Disposed = true;
            }
        }

        private class Test2Controller : IController
        {
            public void Execute(RequestContext requestContext)
            {
            }
        }

        [Test]
        public void CreateController()
        {
            IIocContainerResolver iocContainerResolver = Substitute.For<IIocContainerResolver>();

            Test1Controller test1Controller = new Test1Controller();
            iocContainerResolver.GetInstanceOptionalByName<IController>("Test1")
                .Returns(x => test1Controller);

            Test2Controller test2Controller = new Test2Controller();
            iocContainerResolver.GetInstanceOptionalByName<IController>("Test2")
                .Returns(x => test2Controller);

            LaboCmsControllerFactory factory = new LaboCmsControllerFactory(iocContainerResolver);
            RequestContext requestContext = Substitute.For<RequestContext>();
            
            Assert.AreEqual(test1Controller, factory.CreateController(requestContext, "Test1"));
            Assert.AreEqual(test2Controller, factory.CreateController(requestContext, "Test2"));
        }

        [Test]
        public void ReleaseController()
        {
            IIocContainerResolver iocContainerResolver = Substitute.For<IIocContainerResolver>();

            Test1Controller test1Controller = new Test1Controller();
            LaboCmsControllerFactory factory = new LaboCmsControllerFactory(iocContainerResolver);

            factory.ReleaseController(test1Controller);

            Assert.IsTrue(test1Controller.Disposed);
        }
    }
}
