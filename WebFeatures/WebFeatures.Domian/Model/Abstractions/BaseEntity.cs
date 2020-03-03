namespace WebFeatures.Domian.Model.Abstractions
{
    public abstract class BaseEntity<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}
