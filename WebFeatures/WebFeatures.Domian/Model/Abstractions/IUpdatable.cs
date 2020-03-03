using System;

namespace WebFeatures.Domian.Model.Abstractions
{
    public interface IUpdatable
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
