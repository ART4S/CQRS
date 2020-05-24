using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        private Dictionary<Type, object> _mappings;

        public EntityProfile()
        {
            AddMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void AddMappingsFromAssembly(Assembly assembly)
        {
            _mappings = assembly.GetTypes()
                .Where(t => t.IsSubclassOfGeneric(typeof(EntityMap<>)))
                .Select(t => new
                {
                    EntityType = t.BaseType.GetGenericArguments()[0],
                    Mapping = Activator.CreateInstance(t)
                })
                .ToDictionary(t => t.EntityType, t => t.Mapping);
        }

        public IEntityMap<TEntity> GetMap<TEntity>() where TEntity : class
        {
            _mappings.TryGetValue(typeof(TEntity), out object map);
            return (IEntityMap<TEntity>)map;
        }
    }
}
