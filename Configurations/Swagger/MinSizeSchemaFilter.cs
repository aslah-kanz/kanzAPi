using System.Reflection;
using KanzApi.Common.Validation;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KanzApi.Configurations.Swagger;

public class MinSizeSchemaFilter : ISchemaFilter
{

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        MemberInfo memberInfo = context.MemberInfo;
        if (context.MemberInfo == null) return;

        int value = -1;
        foreach (object attribute in memberInfo.GetCustomAttributes())
        {
            if (attribute is IMinSizeAttribute a && a.Min > value)
            {
                value = a.Min;
            }
        }

        if (value > -1)
        {
            int? lengthValue = null, itemsValue = null, propertiesValue = null;
            if (schema.Type.Equals("array")) itemsValue = value;
            else if (schema.Type.Equals("object")) propertiesValue = value;
            else lengthValue = value;

            schema.MinLength = lengthValue;
            schema.MinItems = itemsValue;
            schema.MinProperties = propertiesValue;
        }
    }
}