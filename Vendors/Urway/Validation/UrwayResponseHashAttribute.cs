using System.ComponentModel.DataAnnotations;
using KanzApi.Extensions;
using KanzApi.Vendors.Urway.Models;

namespace KanzApi.Vendors.Urway.Validation;

[AttributeUsage(AttributeTargets.Class)]
public class UrwayResponseHashAttribute : ValidationAttribute
{

    private ValidationResult? IsValid(IUrwayValidatableResponse value, ValidationContext validationContext)
    {
        IConfiguration configuration = validationContext.GetService<IConfiguration>()!;
        string secretKey = configuration.GetValue<string>("Urway:SecretKey")!;

        if (value.ResponseHash != null)
        {
            string responseHash = (value.TransactionId
            + "|" + secretKey
            + "|" + value.ResponseCode
            + "|" + value.Amount).Sha256();

            if (!responseHash.Equals(value.ResponseHash))
            {
                return new ValidationResult("Response hash mismatch.", ["responseHash"]);
            }
        }

        return ValidationResult.Success;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }
        else if (value is IUrwayValidatableResponse m)
        {
            return IsValid(m, validationContext);
        }
        else throw new NotSupportedException();
    }
}