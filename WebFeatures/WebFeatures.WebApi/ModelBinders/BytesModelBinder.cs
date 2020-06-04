using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebFeatures.WebApi.ModelBinders
{
    internal class BytesModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType != typeof(byte[]))
            {
                return null;
            }

            return new BytesModelBinder();
        }
    }

    internal class BytesModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            string modeName = bindingContext.ModelName;

            var res = await TryGetFirstNotEmptyFileAsync(bindingContext.HttpContext.Request, modeName);
            if (!res.Success)
                return;

            byte[] bytes = await ReadBytesAsync(res.File);

            bindingContext.Result = ModelBindingResult.Success(bytes);
        }

        private async Task<(bool Success, IFormFile File)> TryGetFirstNotEmptyFileAsync(HttpRequest request, string modelName)
        {
            if (request.HasFormContentType)
            {
                IFormCollection form = await request.ReadFormAsync();

                foreach (IFormFile file in form.Files)
                {
                    if (file.Length > 0 && string.Equals(file.Name, modelName, StringComparison.OrdinalIgnoreCase))
                        return (true, file);
                }
            }

            return (false, null);
        }

        private async Task<byte[]> ReadBytesAsync(IFormFile file)
        {
            await using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}
