using System;

namespace WebFeatures.Domian.Entities.Abstractions
{
    /// <summary>
    /// Обновляемая
    /// </summary>
    public interface IUpdatable
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
