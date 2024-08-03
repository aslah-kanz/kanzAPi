using System.ComponentModel.DataAnnotations;
using KanzApi.Resources;

namespace KanzApi.Transaction.Models;

public class CustomerOrderChangeDeliveryOptionRequest
{

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    public int? Id { get; set; }
}
