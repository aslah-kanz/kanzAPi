using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using KanzApi.Common.Models;

namespace KanzApi.Common.Validation;

public class PasswordAttribute : ValidationAttribute
{
    private bool IsValid(string? value)
    {
        return !String.IsNullOrEmpty(value) && Regex.IsMatch(value, "^(?=.*\\d)(?=.*[\\W_]).{6,20}$");
    }

    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return false;
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
