using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WebFeatures.Application.Tests.Common.Extensions
{
	internal static class ScopeExtensions
	{
		public static async Task DisposeAsync(this IServiceScope scope)
		{
			switch (scope)
			{
				case IAsyncDisposable asyncDisposable:
					await asyncDisposable.DisposeAsync();
					break;

				case IDisposable disposable:
					disposable.Dispose();
					break;
			}
		}
	}
}
