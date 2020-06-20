using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;

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
            if (map.IsGenericTypeDefinition)
            {
                return false;
            }

            Type mapInterface = map.GetInterfaces()
                .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityMap<>));

            if (mapInterface == null)
            {
                return false;
            }

            Type entityType = mapInterface.GetGenericArguments()[0];

            if (_mappings.ContainsKey(entityType))
            {
                return false;
            }

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
            if (!_mappings.ContainsKey(typeof(TEntity)))
            {
                TryRegisterMap(typeof(EntityMap<TEntity>));
            }

            return (IEntityMap<TEntity>)_mappings[typeof(TEntity)];
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