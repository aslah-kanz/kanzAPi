using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KanzApi.Common.Models.Binding;

public class UrlDecoderModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult != ValueProviderResult.None && !String.IsNullOrEmpty(valueProviderResult.FirstValue))
        {
            var decodedValue = Uri.UnescapeDataString(valueProviderResult.FirstValue);
            bindingContext.Result = ModelBindingResult.Success(decodedValue);
        }
        else
        {
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}