// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultEmbeddedViewResolver.cs" company="Labo">
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
//   Defines the DefaultEmbeddedViewResolver type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Mvc.EmbeddedViews
{
    using System;
    using System.Reflection;

    /// <summary>
    /// The default embedded view resolver implementation.
    /// </summary>
    public class DefaultEmbeddedViewResolver : IEmbeddedViewResolver
    {
        /// <summary>
        /// Gets the all embedded views by searching all assemblies.
        /// </summary>
        /// <returns>Embedded view table.</returns>
        public EmbeddedViewTable GetEmbeddedViews()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            if (assemblies.Length == 0)
            {
                return null;
            }

            EmbeddedViewTable table = new EmbeddedViewTable();

            for (int i = 0; i < assemblies.Length; i++)
            {
                Assembly assembly = assemblies[i];
                string[] names = GetNamesOfAssemblyResources(assembly);
                if (names == null || names.Length == 0)
                {
                    continue;
                }

                for (int j = 0; j < names.Length; j++)
                {
                    string name = names[j];
                    if (name.IndexOf(".views.", StringComparison.OrdinalIgnoreCase) != -1
                        && name.EndsWith(".cshtml", StringComparison.OrdinalIgnoreCase))
                    {
                        table.AddView(name, assembly.FullName, assembly.Location);
                    }
                }
            }

            return table;
        }

        /// <summary>
        /// Gets all the names of assembly resources.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The names of assembly resources</returns>
        private static string[] GetNamesOfAssemblyResources(Assembly assembly)
        {
            // GetManifestResourceNames will throw a NotSupportedException when run on a dynamic assembly
            try
            {
                if (!assembly.IsDynamic)
                {
                    return assembly.GetManifestResourceNames();
                }
            }
            catch
            {
                // Any exception we fall back to returning an empty array.
            }
            
            return new string[0];
        }
    }
}