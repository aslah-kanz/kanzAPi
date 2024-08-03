using System.ComponentModel.DataAnnotations;
using KanzApi.Resources;
namespace KanzApi.Transaction.Models;

public class PurchaseQuoteAcceptRequest
{

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    public int? ConfirmedQuantity { get; set; }
}
