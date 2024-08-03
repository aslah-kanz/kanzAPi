using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace KanzApi.Common.Validation;

public class ImageFileAttribute : ValidationAttribute
{

    private bool IsValid(IFormFile value)
    {
        return value.ContentType.StartsWith("image/");
    }

    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }
        else if (value is IFormFile v)
        {
            return IsValid(v);
        }
        else if (value is IEnumerable e)
        {
            bool result = true;
            foreach (object o in e)
            {
                result &= IsValid(o);
            }
            return result;
        }
        else throw new NotSupportedException();
    }
}