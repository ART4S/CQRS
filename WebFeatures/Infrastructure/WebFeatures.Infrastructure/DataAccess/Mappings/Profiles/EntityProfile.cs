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

        public bool TryRegisterMap(Type map)
        {
            if (!map.IsSubclassOfGeneric(typeof(EntityMap<>)))
            {
                return false;
            }

            Type entityType = map.BaseType.GetGenericArguments()[0];

            object mapInstance = Activator.CreateInstance(map);

            _mappings[entityType] = mapInstance;

            var entityTypeMap = (SqlMapper.ITypeMap)Activator.CreateInstance(
                typeof(EntityTypeMap<>).MakeGenericType(entityType),
                mapInstance);

            SqlMapper.SetTypeMap(entityType, entityTypeMap);

            return true;
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

    internal static class EntityProfileExtensions
    {
        public static void RegisterMappingsFromAssembly(this EntityProfile profile, Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                profile.TryRegisterMap(type);
            }
        }
    }
}
