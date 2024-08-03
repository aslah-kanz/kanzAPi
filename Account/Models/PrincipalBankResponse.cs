using System.Text.Json.Serialization;
using KanzApi.Common.Models;

namespace KanzApi.Account.Models;

public class PrincipalBankResponse
{

    public int Id { get; set; }

    public string? PaymentMode { get; set; } = "";

    public string? Name { get; set; } = "";

    public string? BeneficiaryName { get; set; } = "";

    public string? City { get; set; } = "";

    public string? AccountNumber { get; set; } = "";

    public string? Iban { get; set; } = "";

    public string? SwiftCode { get; set; } = "";

    public CurrencyItem? Currency { get; set; }

    [JsonPropertyName("proofDocument")]
    public DocumentResponse? Document { get; set; }

}
