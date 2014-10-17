namespace Labo.Cms.Core.Services
{
    using Labo.Cms.Core.Models;

    public interface ICmsService
    {
        Page GetPageBySlug(string slug);
    }
}