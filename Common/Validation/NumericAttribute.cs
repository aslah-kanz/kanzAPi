using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using KanzApi.Common.Models;

namespace KanzApi.Common.Validation;

public class NumericAttribute : ValidationAttribute
{

    private bool IsValid(string? value)
    {
        return String.IsNullOrEmpty(value) || Regex.IsMatch(value, "^[0-9]*$");
    }

    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }
        else if (value is string s)
        {
            return IsValid(s);
        }
        else if (value is LocalizableString lsv)
        {
            return IsValid(lsv.En) && IsValid(lsv.Ar);
        }
        else throw new NotSupportedException();
    }
}