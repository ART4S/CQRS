using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using WebFeatures.Domian.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Profiles
{
    internal class EntityProfile : IEntityProfile
    {
        private Dictionary<Type, IEntityMap> _mappings;

        public EntityProfile()
        {
            AddMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void AddMappingsFromAssembly(Assembly assembly)
        {
            _mappings = assembly.GetTypes()
                .Where(t => !t.IsGenericType && t.GetInterfaces().Any(i => i == typeof(IEntityMap)))
                .Select(t => (IEntityMap)Activator.CreateInstance(t))
                .ToDictionary(m => m.Type, m => m);
        }

        public IEntityMap GetMappingFor<TEntity>() where TEntity : BaseEntity
        {
            _mappings.TryGetValue(typeof(TEntity), out IEntityMap mapping);
            return mapping;
        }
    }
}
