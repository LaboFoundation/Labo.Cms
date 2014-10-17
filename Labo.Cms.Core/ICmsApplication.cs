namespace Labo.Cms.Core
{
    using Labo.Cms.Core.Routing;
    using Labo.Cms.Core.Services;

    public interface ICmsApplication
    {
        IRouteManager RouteManager { get; }

        IRouteProviderManager RouteProviderManager { get; }

        ICmsService CmsService { get; }

        void Initialize();
    }
}