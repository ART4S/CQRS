namespace WebFeatures.Application.Infrastructure.Results
{
    /// <summary>
    /// Void imitation
    /// </summary>
    /// <remarks>Simple return type for commands</remarks>
    public struct Empty
    {
        public static readonly Empty Value = new Empty();
    }
}
