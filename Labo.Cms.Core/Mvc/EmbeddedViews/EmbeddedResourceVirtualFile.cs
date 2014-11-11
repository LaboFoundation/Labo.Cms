// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmbeddedResourceVirtualFile.cs" company="Labo">
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
//   Defines the EmbeddedResourceVirtualFile type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Mvc.EmbeddedViews
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Web.Hosting;

    /// <summary>
    /// Embedded resource virtual file implementation.
    /// </summary>
    public sealed class EmbeddedResourceVirtualFile : VirtualFile
    {
        /// <summary>
        /// The embedded view metadata
        /// </summary>
        private readonly EmbeddedViewMetadata m_EmbeddedViewMetadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceVirtualFile"/> class.
        /// </summary>
        /// <param name="embeddedViewMetadata">The embedded view metadata.</param>
        /// <param name="virtualPath">The virtual path.</param>
        /// <exception cref="System.ArgumentNullException">embeddedViewMetadata</exception>
        public EmbeddedResourceVirtualFile(EmbeddedViewMetadata embeddedViewMetadata, string virtualPath)
            : base(virtualPath)
        {
            if (embeddedViewMetadata == null)
            {
                throw new ArgumentNullException("embeddedViewMetadata");
            }

            m_EmbeddedViewMetadata = embeddedViewMetadata;
        }

        /// <summary>
        /// When overridden in a derived class, returns a read-only stream to the virtual resource.
        /// </summary>
        /// <returns>
        /// A read-only stream to the virtual file.
        /// </returns>
        public override Stream Open()
        {
            Assembly assembly = GetResourceAssembly();
            return assembly == null ? null : assembly.GetManifestResourceStream(m_EmbeddedViewMetadata.Name);
        }

        /// <summary>
        /// Gets the resource assembly.
        /// </summary>
        /// <returns>The resource assembly.</returns>
        private Assembly GetResourceAssembly()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < assemblies.Length; i++)
            {
                Assembly assembly = assemblies[i];
                if (string.Equals(assembly.FullName, m_EmbeddedViewMetadata.AssemblyFullName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return assembly;
                }
            }

            return null;
        }
    }
}