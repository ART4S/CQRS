using System;

namespace WebFeatures.Domian.Entities.Abstractions
{
    public interface IUpdatable
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
