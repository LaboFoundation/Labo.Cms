// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmbeddedViewVirtualPathProvider.cs" company="Labo">
//   The MIT License (MIT)
//   
//   Copyright (c) 2014 Bora Akgun
//   
//   Permission is hereby granted, free of charge, to any person obtaining a copy of
//   this software and associated documentation files (the "Software"), to deal in
//   the Software without restriction, including without limitation the rights to
//   use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//   the Software, and to permit persons to whom the Software is furnished to do so,
//   subject to the following conditions:
//   
//   The above copyright notice and this permission notice shall be included in all
//   copies or substantial portions of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
//   FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
//   COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//   IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//   CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary>
//   Defines the EmbeddedViewVirtualPathProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Mvc.EmbeddedViews
{
    using System;
    using System.Collections;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Hosting;

    /// <summary>
    /// The embedded view partial path provider class.
    /// </summary>
    public class EmbeddedViewVirtualPathProvider : VirtualPathProvider
    {
        /// <summary>
        /// The embedded views
        /// </summary>
        private readonly EmbeddedViewTable m_EmbeddedViews;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedViewVirtualPathProvider"/> class.
        /// </summary>
        /// <param name="embeddedViews">
        /// The embedded views.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// embeddedViews
        /// </exception>
        public EmbeddedViewVirtualPathProvider(EmbeddedViewTable embeddedViews)
        {
            if (embeddedViews == null)
            {
                throw new ArgumentNullException("embeddedViews");
            }

            m_EmbeddedViews = embeddedViews;
        }

        /// <summary>
        /// Gets a value that indicates whether a file exists in the virtual file system.
        /// </summary>
        /// <returns>
        /// true if the file exists in the virtual file system; otherwise, false.
        /// </returns>
        /// <param name="virtualPath">The path to the virtual file.</param>
        public override bool FileExists(string virtualPath)
        {
            return IsEmbeddedView(virtualPath) || Previous.FileExists(virtualPath);
        }

        /// <summary>
        /// Gets a virtual file from the virtual file system.
        /// </summary>
        /// <returns>
        /// A descendent of the <see cref="T:System.Web.Hosting.VirtualFile"/> class that represents a file in the virtual file system.
        /// </returns>
        /// <param name="virtualPath">The path to the virtual file.</param>
        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsEmbeddedView(virtualPath))
            {
                string virtualPathAppRelative = VirtualPathUtility.ToAppRelative(virtualPath);
                string fullyQualifiedViewName = GetFullyQualifiedViewName(virtualPathAppRelative);

                EmbeddedViewMetadata embeddedViewMetadata = m_EmbeddedViews.FindEmbeddedView(fullyQualifiedViewName);
                return new EmbeddedResourceVirtualFile(embeddedViewMetadata, virtualPath);
            }

            return Previous.GetFile(virtualPath);
        }

        /// <summary>
        /// Creates a cache dependency based on the specified virtual paths.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Caching.CacheDependency"/> object for the specified virtual resources.
        /// </returns>
        /// <param name="virtualPath">The path to the primary virtual resource.</param><param name="virtualPathDependencies">An array of paths to other resources required by the primary virtual resource.</param><param name="utcStart">The UTC time at which the virtual resources were read.</param>
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            EmbeddedViewMetadata embeddedViewMetadata = GetEmbeddedView(virtualPath);
            return embeddedViewMetadata != null ? (string.IsNullOrWhiteSpace(embeddedViewMetadata.AssemblyLocation) ? null : new CacheDependency(embeddedViewMetadata.AssemblyLocation, utcStart)) : Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        /// <summary>
        /// Gets the fully qualified view name.
        /// </summary>
        /// <param name="virtualPathAppRelative">The virtual path app relative.</param>
        /// <returns>The fully qualified view name</returns>
        private static string GetFullyQualifiedViewName(string virtualPathAppRelative)
        {
            int lastIndexOfSlash = virtualPathAppRelative.LastIndexOf("/", StringComparison.OrdinalIgnoreCase);
            return virtualPathAppRelative.Substring(lastIndexOfSlash + 1, virtualPathAppRelative.Length - 1 - lastIndexOfSlash);
        }

        /// <summary>
        /// Determines whether [is embedded view] [the specified virtual path].
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns>
        ///   <c>true</c> if [is embedded view] [the specified virtual path]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsEmbeddedView(string virtualPath)
        {
            EmbeddedViewMetadata embeddedViewMetadata = GetEmbeddedView(virtualPath);
            return embeddedViewMetadata != null;
        }

        /// <summary>
        /// Gets the embedded view meta data.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns>The embedded view meta data</returns>
        private EmbeddedViewMetadata GetEmbeddedView(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
            {
                return null;
            }

            string virtualPathAppRelative = VirtualPathUtility.ToAppRelative(virtualPath);
            if (!virtualPathAppRelative.StartsWith("~/Views/", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            string fullyQualifiedViewName = GetFullyQualifiedViewName(virtualPathAppRelative);

            return m_EmbeddedViews.FindEmbeddedView(fullyQualifiedViewName);
        }
    }
}