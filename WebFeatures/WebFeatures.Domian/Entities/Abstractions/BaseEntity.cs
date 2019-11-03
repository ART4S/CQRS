using System;

namespace WebFeatures.Domian.Entities.Abstractions
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    public abstract class BaseEntity<TId> : IEntity<TId> 
        where TId : struct, IEquatable<TId>
    {
        public TId Id { get; set; }
    }
}
