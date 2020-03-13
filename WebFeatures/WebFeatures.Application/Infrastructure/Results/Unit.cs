namespace WebFeatures.Application.Infrastructure.Results
{
    /// <summary>
    /// Void imitation
    /// </summary>
    /// <remarks>Simple return type for commands</remarks>
    public struct Unit
    {
        public static readonly Unit Value = new Unit();
    }
}
