using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace WebFeatures.Application.Tests.Common.Extensions
{
    internal static class ScopeExtensions
    {
        public static async Task DisposeAsync(this IServiceScope scope)
        {
            switch (scope)
            {
                case IAsyncDisposable ad:
                    await ad.DisposeAsync();
                    break;

                case IDisposable d:
                    d.Dispose();
                    break;
            }
        }
    }
}