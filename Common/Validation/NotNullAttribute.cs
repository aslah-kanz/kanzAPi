using System.ComponentModel.DataAnnotations;
using KanzApi.Common.Models;

namespace KanzApi.Common.Validation;

public class NotNullAttribute : ValidationAttribute, INonNullableAttribute
{

    public override bool IsValid(object? value)
    {
        if (value is LocalizableString v)
        {
            return IsValid(v.En) && IsValid(v.Ar);
        }
        else
        {
            return value != null;
        }
    }
}