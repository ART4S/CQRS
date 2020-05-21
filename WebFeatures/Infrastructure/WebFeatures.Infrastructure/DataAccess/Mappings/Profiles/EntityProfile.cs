using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Helpers;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Profiles
{
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

        public EntityMap<TEntity> GetMappingFor<TEntity>() where TEntity : class
        {
            _mappings.TryGetValue(typeof(TEntity), out object mapping);
            return (EntityMap<TEntity>)mapping;
        }
    }
}
