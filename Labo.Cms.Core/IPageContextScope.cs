namespace Labo.Cms.Core
{
    using System;

    public interface IPageContextScope : IDisposable
    {
        IPageContext PageContext { get; }
    }
}