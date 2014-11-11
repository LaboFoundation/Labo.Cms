// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmbeddedViewTable.cs" company="Labo">
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
//   Defines the EmbeddedViewTable type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Mvc.EmbeddedViews
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The embedded view table.
    /// </summary>
    [Serializable]
    public class EmbeddedViewTable
    {
        /// <summary>
        /// The lock
        /// </summary>
        private static readonly object s_Lock = new object();
        
        /// <summary>
        /// The view cache
        /// </summary>
        private readonly IDictionary<string, EmbeddedViewMetadata> m_ViewCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedViewTable"/> class.
        /// </summary>
        public EmbeddedViewTable()
        {
            m_ViewCache = new SortedList<string, EmbeddedViewMetadata>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Adds the view.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="assemblyLocation">The assembly location.</param>
        public void AddView(string viewName, string assemblyName, string assemblyLocation = null)
        {
            lock (s_Lock)
            {
                m_ViewCache[viewName] = new EmbeddedViewMetadata { Name = viewName, AssemblyFullName = assemblyName, AssemblyLocation = assemblyLocation };
            }
        }

        /// <summary>
        /// Gets the views.
        /// </summary>
        /// <value>
        /// The views.
        /// </value>
        public IList<EmbeddedViewMetadata> Views
        {
            get
            {
                return m_ViewCache.Values.ToList();
            }
        }

        /// <summary>
        /// Determines whether [contains embedded view] [the specified fully qualified view name].
        /// </summary>
        /// <param name="fullyQualifiedViewName">Name of the fully qualified view.</param>
        /// <returns>
        ///   <c>true</c> if [contains embedded view] [the specified fully qualified view name]; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsEmbeddedView(string fullyQualifiedViewName)
        {
            EmbeddedViewMetadata foundView = FindEmbeddedView(fullyQualifiedViewName);
            return foundView != null;
        }

        /// <summary>
        /// Finds the embedded view.
        /// </summary>
        /// <param name="fullyQualifiedViewName">Name of the fully qualified view.</param>
        /// <returns>The embedded view metadata.</returns>
        public EmbeddedViewMetadata FindEmbeddedView(string fullyQualifiedViewName)
        {
            if (string.IsNullOrWhiteSpace(fullyQualifiedViewName))
            {
                return null;
            }

            EmbeddedViewMetadata viewMetadata;

            return m_ViewCache.TryGetValue(fullyQualifiedViewName, out viewMetadata) ? viewMetadata : null;
        }
    }
}