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
        ILifeCycleBuilder WithInterface(Type interfaceType);
    }

    public interface ILifeCycleBuilder
    {
        void SetLifetime(ServiceLifetime lifetime);
    }

    internal class TypesRegistrationBuilder : ITypesRegistrationBuilder
    {
        private readonly IServiceCollection _services;
        private IEnumerable<Type> _types;

        public TypesRegistrationBuilder(IServiceCollection services, IEnumerable<Type> types)
        {
            _services = services;
            _types = types;
        }

        public ILifeCycleBuilder WithInterface(Type interfaceType)
        {
            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException($"{interfaceType} should be an interface type");
            }

            var serviceDescriptions = GetInterfaceServices(interfaceType);

            return new LifeCycleBuilder(_services, serviceDescriptions);
        }

        private IEnumerable<ServiceDescription> GetInterfaceServices(Type interfaceType)
        {
            foreach (Type type in _types)
            {
                IEnumerable<Type> serviceTypes;

                if (interfaceType.IsGenericType)
                {
                    serviceTypes = type.GetInterfaces()
                        .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
                }
                else
                {
                    serviceTypes = type.GetInterfaces()
                        .Where(x => x == interfaceType);
                }

                foreach (Type serviceType in serviceTypes)
                {
                    yield return new ServiceDescription(serviceType, type);
                }
            }
        }
    }

    internal class LifeCycleBuilder : ILifeCycleBuilder
    {
        private readonly IServiceCollection _services;
        private IEnumerable<ServiceDescription> _serviceDescriptions;

        public LifeCycleBuilder(IServiceCollection services, IEnumerable<ServiceDescription> serviceDescriptions)
        {
            _services = services;
            _serviceDescriptions = serviceDescriptions;
        }

        public void SetLifetime(ServiceLifetime lifetime)
        {
            foreach (var d in _serviceDescriptions)
            {
                _services.Add(new ServiceDescriptor(
                    d.ServiceType,
                    d.ImplementationType,
                    lifetime));
            }
        }
    }

    internal class ServiceDescription
    {
        public Type ServiceType { get; }
        public Type ImplementationType { get; }

        public ServiceDescription(Type serviceType, Type implementationType)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
        }
    }
}