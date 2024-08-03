using System.Reflection;
using KanzApi.Common.Validation;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KanzApi.Configurations.Swagger;

public class MaxSizeSchemaFilter : ISchemaFilter
{

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        MemberInfo memberInfo = context.MemberInfo;
        if (context.MemberInfo == null) return;

        int? value = null;
        foreach (object attribute in memberInfo.GetCustomAttributes())
        {
            if (attribute is IMaxSizeAttribute a
            && (value == null || a.Max < value))
            {
                value = a.Max;
            }
        }

        if (value != null)
        {
            int? lengthValue = null, itemsValue = null, propertiesValue = null;
            if (schema.Type.Equals("array")) itemsValue = value;
            else if (schema.Type.Equals("object")) propertiesValue = value;
            else lengthValue = value;

            schema.MaxLength = lengthValue;
            schema.MaxItems = itemsValue;
            schema.MaxProperties = propertiesValue;
        }
    }
}