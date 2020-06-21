using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebFeatures.Common.Extensions
{
    public static class DIExtensions
    {
        public static ITypesRegistrationBuilder RegisterTypesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            return new TypesRegistrationBuilder(services, assembly.GetTypes());
        }
    }

    public interface ITypesRegistrationBuilder
    {
        ITypesRegistrationBuilder Where(Func<Type, bool> typeSelector);

        ILifeCycleBuilder As(Func<Type, Type> implementationTypeSelector);

        ILifeCycleBuilder As(Func<Type, IEnumerable<Type>> implementationTypesSelector);
    }

    public interface ILifeCycleBuilder
    {
        void WithLifetime(ServiceLifetime lifetime);
    }

    internal class TypesRegistrationBuilder : ITypesRegistrationBuilder
    {
        private readonly IServiceCollection _services;
        private readonly IEnumerable<Type> _types;

        public TypesRegistrationBuilder(IServiceCollection services, IEnumerable<Type> types)
        {
            _services = services;
            _types = types;
        }

        public ILifeCycleBuilder As(Func<Type, Type> implementationTypeSelector)
        {
            return new LifeCycleBuilder(_services, _types, x => new[] { implementationTypeSelector(x) });
        }

        public ILifeCycleBuilder As(Func<Type, IEnumerable<Type>> implementationTypesSelector)
        {
            return new LifeCycleBuilder(_services, _types, implementationTypesSelector);
        }

        public ITypesRegistrationBuilder Where(Func<Type, bool> typeSelector)
        {
            return new TypesRegistrationBuilder(_services, _types.Where(typeSelector));
        }
    }

    internal class LifeCycleBuilder : ILifeCycleBuilder
    {
        private readonly IServiceCollection _services;
        private readonly IEnumerable<Type> _serviceTypes;
        private readonly Func<Type, IEnumerable<Type>> _implementationTypesSelector;

        public LifeCycleBuilder(IServiceCollection services, IEnumerable<Type> serviceTypes, Func<Type, IEnumerable<Type>> implementationTypesSelector)
        {
            _services = services;
            _serviceTypes = serviceTypes;
            _implementationTypesSelector = implementationTypesSelector;
        }

        public void WithLifetime(ServiceLifetime lifetime)
        {
            foreach (Type serviceType in _serviceTypes)
            {
                IEnumerable<Type> implementationTypes = _implementationTypesSelector(serviceType);

                foreach (Type implementationType in implementationTypes)
                {
                    _services.Add(new ServiceDescriptor(serviceType, implementationType, lifetime));
                }
            }
        }
    }
}