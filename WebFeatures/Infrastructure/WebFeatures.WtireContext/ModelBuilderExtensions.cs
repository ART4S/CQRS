using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using WebFeatures.Domian.Attibutes;

namespace WebFeatures.WriteContext
{
    internal static class ModelBuilderExtensions
    {
        public static void ApplyTableNames(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableNameAttr = entity.ClrType.GetCustomAttribute<EntityMetadataAttribute>();
                if (tableNameAttr != null)
                {
                    modelBuilder.Entity(entity.Name).ToTable(tableNameAttr.Name);
                }
            }
        }
    }
}
