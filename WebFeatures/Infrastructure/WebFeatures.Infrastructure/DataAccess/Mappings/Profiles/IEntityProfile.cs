using WebFeatures.Domian.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Profiles
{
    internal interface IEntityProfile
    {
        EntityMap<TEntity> GetMappingFor<TEntity>() where TEntity : Entity;
    }
}
