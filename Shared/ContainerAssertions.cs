﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Autofac;
using Autofac.Util;
using FluentAssertions.Primitives;

namespace FluentAssertions.Autofac
{
    /// <summary>
    ///     Contains a number of methods to assert that an <see cref="IContainer" /> is in the expected state.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public class ContainerAssertions : ReferenceTypeAssertions<IContainer, ContainerAssertions>
    {
        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
#if !PORTABLE && !CORE_CLR
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
        protected override string Context => nameof(IContainer);

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainerAssertions" /> class.
        /// </summary>
        /// <param name="container">The subject</param>
        public ContainerAssertions(IContainer container)
        {
            Subject = container;
        }

        /// <summary>
        ///     Returns an <see cref="ContainerRegistrationAssertions"/> object that can be used to assert the current <see cref="IContainer"/>.
        /// </summary>
        public ContainerRegistrationAssertions Have()
        {
            return new ContainerRegistrationAssertions(Subject);
        }

        /// <summary>
        ///     Returns an <see cref="ResolveAssertions"/> object that can be used to assert the current <see paramref="serviceType"/>.
        /// </summary>
        public ResolveAssertions Resolve(Type serviceType)
        {
            return new ResolveAssertions(Subject, serviceType);
        }

        /// <summary>
        ///     Returns an <see cref="ResolveAssertions"/> object that can be used to assert the current <see typeparamref="TService"/>.
        /// </summary>
        public ResolveAssertions Resolve<TService>()
        {
            return Resolve(typeof(TService));
        }

        /// <summary>
        ///   Asserts the specified type has been registered with auto activation on the current <see cref="IContainer"/>.
        /// </summary>
        public void AutoActivate(Type type)
        {
            Subject.AssertAutoActivates(type);
        }

        /// <summary>
        ///   Asserts the specified type has been registered with auto activation on the current <see cref="IContainer"/>.
        /// </summary>
        public void AutoActivate<TService>()
        {
            AutoActivate(typeof(TService));
        }

        /// <summary>
        ///     Returns an <see cref="TypeScanningAssertions"/> object that can be used to assert registered types on the current <see cref="MockContainerBuilder"/>.
        /// </summary>
        /// <param name="assemblies"></param>
        public TypeScanningAssertions RegisterAssemblyTypes(params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(assembly => assembly.GetLoadableTypes());
            return new TypeScanningAssertions(Subject, types);
        }

        /// <summary>
        ///     Returns an <see cref="TypeScanningAssertions"/> object that can be used to assert registered types on the current <see cref="MockContainerBuilder"/>.
        /// </summary>
        /// <param name="types"></param>
        public TypeScanningAssertions RegisterTypes(IEnumerable<Type> types)
        {
            return new TypeScanningAssertions(Subject, types);
        }
    }
}
 