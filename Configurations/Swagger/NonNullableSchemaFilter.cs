using System.Reflection;
using KanzApi.Common.Validation;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KanzApi.Configurations.Swagger;

public class NonNullableSchemaFilter : ISchemaFilter
{

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        MemberInfo memberInfo = context.MemberInfo;
        if (context.MemberInfo == null) return;

        Type type = typeof(INonNullableAttribute);
        CustomAttributeData? attribute = memberInfo.CustomAttributes
        .Where(p => type.IsAssignableFrom(p.AttributeType))
        .FirstOrDefault();

        if (attribute != null)
        {
            schema.Nullable = false;
        }
    }
}