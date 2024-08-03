namespace KanzApi.Vendors.Urway.Models;

public interface IUrwayValidatableResponse
{

    string? TransactionId { get; }

    string? ResponseCode { get; }

    string? Amount { get; }

    string? ResponseHash { get; }
}
