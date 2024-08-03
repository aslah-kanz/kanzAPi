using KanzApi.Transaction.Models;
using KanzApi.Vendors.Oto.Models;

namespace KanzApi.Vendors.Oto.Services;

public interface IOtoCheckoutService
{

    OtoDeliveryFeeResponse CheckOtoDeliveryFee(OtoDeliveryFeeRequest request);

    OtoDeliveryFeeResponse CheckOtoDeliveryFee(DeliveryDetail deliveryDetail);
}
