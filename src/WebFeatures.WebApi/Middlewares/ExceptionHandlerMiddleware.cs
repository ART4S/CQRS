using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.WebApi.Exceptions;

namespace WebFeatures.WebApi.Middlewares
{
	internal class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(ex, context);
			}
		}

		private static async Task HandleExceptionAsync(Exception exception, HttpContext context)
		{
			switch (exception)
			{
				case ValidationException validation:
				{
					context.Response.StatusCode = StatusCodes.Status400BadRequest;
					context.Response.ContentType = MediaTypeNames.Application.Json;

					string body = JsonConvert.SerializeObject(validation.Error);

					await context.Response.WriteAsync(body);

					return;
				}

				case AccessDeniedException _:
				{
					context.Response.StatusCode = StatusCodes.Status403Forbidden;

					return;
				}

				default:
				{
					context.Response.StatusCode = StatusCodes.Status500InternalServerError;
					context.Response.ContentType = MediaTypeNames.Text.Plain;

					var logger = context.RequestServices.GetService<ILogger<ExceptionHandlerMiddleware>>();

					logger.LogError(exception.Message, exception);

					await context.Response.WriteAsync("Something went wrong");

					return;
				}
			}
		}
	}
}
