using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WebFeatures.Common.Extensions
{
	public static class DIExtensions
	{
		public static ITypesRegistrationBuilder RegisterTypesFromAssembly(
			this IServiceCollection services,
			Assembly assembly)
		{
			return new TypesRegistrationBuilder(services, assembly.GetTypes());
		}
	}

	public interface ITypesRegistrationBuilder
	{
		ITypesRegistrationBuilder Where(Func<Type, bool> typeSelector);

		ILifeCycleBuilder As(Func<Type, IEnumerable<Type>> implementationTypesSelector);
	}

	public interface ILifeCycleBuilder
	{
		void WithLifetime(ServiceLifetime lifetime);
	}

	internal class TypesRegistrationBuilder : ITypesRegistrationBuilder
	{
		private readonly IEnumerable<Type> _implementationTypes;
		private readonly IServiceCollection _services;

		public TypesRegistrationBuilder(IServiceCollection services, IEnumerable<Type> implementationTypes)
		{
			_services = services;
			_implementationTypes = implementationTypes;
		}

		public ILifeCycleBuilder As(Func<Type, IEnumerable<Type>> serviceTypesSelector)
		{
			return new LifeCycleBuilder(_services, _implementationTypes, serviceTypesSelector);
		}

		public ITypesRegistrationBuilder Where(Func<Type, bool> predicate)
		{
			return new TypesRegistrationBuilder(_services, _implementationTypes.Where(predicate));
		}
	}

	internal class LifeCycleBuilder : ILifeCycleBuilder
	{
		private readonly IEnumerable<Type> _implementationTypes;
		private readonly IServiceCollection _services;
		private readonly Func<Type, IEnumerable<Type>> _serviceTypesSelector;

		public LifeCycleBuilder(
			IServiceCollection services,
			IEnumerable<Type> implementationTypes,
			Func<Type, IEnumerable<Type>> serviceTypesSelector)
		{
			_services = services;
			_implementationTypes = implementationTypes;
			_serviceTypesSelector = serviceTypesSelector;
		}

		public void WithLifetime(ServiceLifetime lifetime)
		{
			foreach (Type implementationType in _implementationTypes)
			{
				IEnumerable<Type> serviceTypes = _serviceTypesSelector(implementationType);

				foreach (Type serviceType in serviceTypes)
				{
					_services.Add(new ServiceDescriptor(serviceType, implementationType, lifetime));
				}
			}
		}
	}
}
