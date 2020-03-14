using System;

namespace WebFeatures.Domian.Common
{
    public interface IUpdatable
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
