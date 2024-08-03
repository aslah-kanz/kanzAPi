using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Account.Entities;

[Table("PrincipalBank", Schema = "Account")]
[Index(nameof(Iban), IsUnique = true)]
public class PrincipalBank : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(100)]
    public string? Code { get; set; }

    private int? _principalId;

    [Required]
    public int? PrincipalId { get { return _principalId; } set { _principalId = value; } }

    private Principal? _principal;

    public virtual Principal? Principal
    { get { return _principal; } set { _principalId = value?.Id; _principal = value; } }

    private long? _documentId;

    public long? DocumentId { get { return _documentId; } set { _documentId = value; } }

    private Document? _document;

    public virtual Document? Document
    { get { return _document; } set { _documentId = value?.Id; _document = value; } }

    private int? _currencyId;

    public int? CurrencyId { get { return _currencyId; } set { _currencyId = value; } }

    private Currency? _currency;

    public virtual Currency? Currency
    { get { return _currency; } set { _currencyId = value?.Id; _currency = value; } }

    [Required]
    [MaxLength(125)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(125)]
    public string? City { get; set; }

    [Required]
    [MaxLength(125)]
    public string? PaymentMode { get; set; }

    [Required]
    [MaxLength(125)]
    public string? BeneficiaryName { get; set; }

    [Required]
    [MaxLength(125)]
    public string? Iban { get; set; }

    [Required]
    [MaxLength(125)]
    public string? AccountNumber { get; set; }

    [Required]
    [MaxLength(125)]
    public string? SwiftCode { get; set; }

    public static Expression<Func<PrincipalBank, bool>> QNameEquals(string value)
    {
        return arg => arg.Name!.Equals(value);
    }

    public static Expression<Func<PrincipalBank, bool>> QAccountNumberEquals(string value)
    {
        return arg => arg.AccountNumber!.Equals(value);
    }

    public static Expression<Func<PrincipalBank, bool>> QIbanEquals(string value)
    {
        return arg => arg.Iban!.Equals(value);
    }

    public static Expression<Func<PrincipalBank, bool>> QBeneficiaryNameContains(string value)
    {
        return arg => arg.BeneficiaryName!.Contains(value);
    }

    public static Expression<Func<PrincipalBank, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }
}
