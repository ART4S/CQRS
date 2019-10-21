using System;

namespace WebFeatures.Domian.Entities.Abstractions
{
    /// <summary>
    /// Обновляемая сущность
    /// </summary>
    public interface IUpdatable
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
