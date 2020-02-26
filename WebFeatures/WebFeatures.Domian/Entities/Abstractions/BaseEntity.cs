namespace WebFeatures.Domian.Entities.Abstractions
{
    public abstract class BaseEntity<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}
