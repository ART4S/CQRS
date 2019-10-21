namespace WebFeatures.Domian.Entities.Abstractions
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
