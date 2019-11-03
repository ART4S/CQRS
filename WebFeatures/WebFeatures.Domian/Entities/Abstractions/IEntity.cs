using System;

namespace WebFeatures.Domian.Entities.Abstractions
{
    /// <summary>
    /// Сущность
    /// </summary>
    public interface IEntity<TId> where TId : struct, IEquatable<TId>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        TId Id { get; set; }
    }
}
