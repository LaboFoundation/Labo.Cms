// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyUtils.cs" company="Labo">
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
//   Defines the AssemblyUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// The assembly operations helper class.
    /// </summary>
    public static class AssemblyUtils
    {  
        /// <summary>
        /// Finds the types that is or inherits the specified type in the specified assemblies.
        /// </summary>
        /// <param name="assignTypeFrom">The assign type from.</param>
        /// <param name="assemblies">The assemblies.</param>
        /// <param name="onlyConcreteClasses">if set to <c>true</c> [only concrete classes].</param>
        /// <returns>The found types.</returns>
        public static IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            IList<Type> result = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                for (int i = 0; i < types.Length; i++)
                {
                    Type type = types[i];
                    if (assignTypeFrom.IsAssignableFrom(type) || (assignTypeFrom.IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(type, assignTypeFrom)))
                    {
                        if (!type.IsInterface)
                        {
                            if (onlyConcreteClasses)
                            {
                                if (type.IsClass && !type.IsAbstract)
                                {
                                    result.Add(type);
                                }
                            }
                            else
                            {
                                result.Add(type);
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Does the type implement open generic.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="openGeneric">The open generic.</param>
        /// <returns><c>true</c> if the specified type is open generic, otherwise <c>false</c>.</returns>
        public static bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            Type genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
            Type[] implementedInterfaces = type.FindInterfaces((objType, objCriteria) => true, null);
            for (int i = 0; i < implementedInterfaces.Length; i++)
            {
                Type implementedInterface = implementedInterfaces[i];
                if (!implementedInterface.IsGenericType)
                {
                    continue;
                }

                return genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
            }

            return false;
        }
    }
}
