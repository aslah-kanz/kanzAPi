using KanzApi.Account.Entities;
using KanzApi.Vendors.Oto.Models;

namespace KanzApi.Vendors.Oto.Services;

public interface IOtoPickupLocationService
{

    OtoPickupLocationResponse Create(OtoPickupLocationRequest request);

    OtoPickupLocationResponse Create(Store store);

    OtoPickupLocationResponse Update(OtoPickupLocationRequest request);

    OtoPickupLocationResponse Update(Store store);
}
