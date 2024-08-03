using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using KanzApi.Common.Models;
using KanzApi.Extensions;

namespace KanzApi.Common.Validation;

public class MaxSizeAttribute(int max) : ValidationAttribute, IMaxSizeAttribute
{

    private int _max = max;
    public int Max { get { return _max; } }

    private bool IsValid(int value)
    {
        return value <= _max;
    }

    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }
        else if (value is string sv)
        {
            return IsValid(sv.Length);
        }
        else if (value is ICollection cv)
        {
            return IsValid(@cv.Count);
        }
        else if (value is IEnumerable ev)
        {
            return IsValid(ev.Count());
        }
        else if (value is LocalizableString lsv)
        {
            return IsValid(lsv.En) && IsValid(lsv.Ar);
        }
        else throw new NotSupportedException();
    }

    public override string FormatErrorMessage(string name)
    {
        return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _max);
    }
}