using Dapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Profiles
{
    internal interface IEntityProfile
    {
        IEntityMap<TEntity> GetMap<TEntity>() where TEntity : class;
    }

    internal class EntityProfile : IEntityProfile
    {
        private readonly Dictionary<Type, object> _mappings = new Dictionary<Type, object>();

        public void AddMappingsFromAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsGenericType)
                    continue;

                if (!type.IsSubclassOfGeneric(typeof(EntityMap<>)))
                    continue;

                Type entityType = type.BaseType.GetGenericArguments()[0];

                object entityMap = Activator.CreateInstance(type);

                _mappings[entityType] = entityMap;

                var entityTypeMap = (SqlMapper.ITypeMap)Activator.CreateInstance(
                    typeof(EntityTypeMap<>).MakeGenericType(entityType),
                    entityMap);

                SqlMapper.SetTypeMap(entityType, entityTypeMap);
            }
        }

        public IEntityMap<TEntity> GetMap<TEntity>() where TEntity : class
        {
            if (!_mappings.TryGetValue(typeof(TEntity), out object map))
            {
                throw new InvalidOperationException($"Mapping for type '{typeof(TEntity).Name}' is missing");
            }

            return (IEntityMap<TEntity>)map;
        }
    }
}
