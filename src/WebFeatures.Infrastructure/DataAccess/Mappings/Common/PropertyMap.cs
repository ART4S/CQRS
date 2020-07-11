using System;
using System.Linq.Expressions;
using System.Reflection;
using WebFeatures.Common;
using WebFeatures.Infrastructure.DataAccess.Extensions;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
	internal interface IPropertyMap<in TEntity>
		where TEntity : class
	{
		string ColumnName { get; }
		string PropertyAlias { get; }
		PropertyInfo Property { get; }
		object GetValue(TEntity entity);
		void SetValue(TEntity entity, object value);
	}

	internal partial class PropertyMap<TEntity> : IPropertyMap<TEntity>
		where TEntity : class
	{
		private readonly Func<TEntity, object> _getter;
		private readonly Action<TEntity, object> _setter;

		public PropertyMap(Expression<Func<TEntity, object>> propertyCall)
		{
			Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

			_getter = propertyCall.Compile();
			_setter = propertyCall.CreateSetter();
			
			PropertyAlias = ColumnName = propertyCall.GetPropertyName();
			Property = propertyCall.GetFirstProperty();
		}

		public PropertyMap(PropertyInfo property)
		{
			Guard.ThrowIfNull(property, nameof(property));

			_getter = property.CreateGetter<TEntity>();
			_setter = property.CreateSetter<TEntity>();
			
			PropertyAlias = ColumnName = property.Name;
			Property = property;
		}

		public string ColumnName { get; private set; }
		public string PropertyAlias { get; }
		public PropertyInfo Property { get; }

		public object GetValue(TEntity entity)
		{
			return _getter(entity);
		}

		public void SetValue(TEntity entity, object value)
		{
			_setter(entity, value);
		}
	}
}
