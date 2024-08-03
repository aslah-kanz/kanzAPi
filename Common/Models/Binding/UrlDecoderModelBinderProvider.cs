using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KanzApi.Common.Models.Binding;

public class UrlDecoderModelBinderProvider : IModelBinderProvider
{

    private readonly IModelBinder _modelBinder = new UrlDecoderModelBinder();

    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return context.Metadata.ModelType == typeof(string)
        ? _modelBinder : null;
    }
}

