using AutoMapper;

namespace WebFeatures.Application.Infrastructure.Mappings
{
    public interface IHasMappings
    {
        void ApplyMappings(Profile profile);
    }
}
