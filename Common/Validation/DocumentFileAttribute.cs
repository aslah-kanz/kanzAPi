using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace KanzApi.Common.Validation;

public class DocumentFileAttribute : ValidationAttribute
{

    private readonly string[] _allowedTypes = [
        "application/pdf",
        "application/msword",
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
    ];

    private bool IsValid(IFormFile value)
    {
        string type = value.ContentType;
        return _allowedTypes.FirstOrDefault(t => type.Equals(t)) != null;
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