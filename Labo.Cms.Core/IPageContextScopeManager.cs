namespace Labo.Cms.Core
{
    using System.Web.Routing;

    public interface IPageContextScopeManager
    {
        IPageContextScope CreatePageContextScope(RequestContext requestContext);
    }
}