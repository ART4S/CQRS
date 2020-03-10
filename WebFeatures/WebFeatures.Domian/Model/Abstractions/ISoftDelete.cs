namespace WebFeatures.Domian.Model.Abstractions
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
