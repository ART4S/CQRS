using System;
using System.Linq;
using System.Reflection;
using Dapper;
using WebFeatures.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
	internal class EntityTypeMap<TEntity> : SqlMapper.ITypeMap
		where TEntity : class
	{
		private static readonly ConstructorInfo DefaultConstructor =
			typeof(TEntity).GetConstructor(Type.EmptyTypes);

		private readonly EntityMemberMap<TEntity>[] _members;

		public EntityTypeMap(IEntityMap<TEntity> entityMap)
		{
			_members = entityMap.Properties
			   .Select(x => new EntityMemberMap<TEntity>(x))
			   .ToArray();
		}

		public ConstructorInfo FindConstructor(string[] names, Type[] types)
		{
			throw new NotImplementedException();
		}

		public ConstructorInfo FindExplicitConstructor()
		{
			return DefaultConstructor;
		}

		public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
		{
			throw new NotImplementedException();
		}

		public SqlMapper.IMemberMap GetMember(string columnName)
		{
			Guard.ThrowIfNull(columnName, nameof(columnName));

			EntityMemberMap<TEntity> member = 
				_members.FirstOrDefault(x => string.Equals(x.ColumnName, columnName)) ?? 	                                  
				_members.FirstOrDefault(x => string.Equals(x.ColumnName, columnName, StringComparison.OrdinalIgnoreCase));

			if (member == null) throw new ArgumentException($"Member for column '{columnName}' is missing");

			return member;
		}
	}
}
