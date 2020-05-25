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

        public EntityProfile()
        {
            AddMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void AddMappingsFromAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsGenericType)
                    continue;

                Type mapInterface = type.GetInterfaces()
                    .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityMap<>));

                if (mapInterface == null)
                    continue;

                Type entityType = mapInterface.GetGenericArguments()[0];

                _mappings[entityType] = Activator.CreateInstance(type);
            }
        }

        public IEntityMap<TEntity> GetMap<TEntity>() where TEntity : class
        {
            Type entityType = typeof(TEntity);

            if (!_mappings.ContainsKey(entityType))
            {
                _mappings[entityType] = new EntityMap<TEntity>();
            }

            return (IEntityMap<TEntity>)_mappings[entityType];
        }
    }
}
