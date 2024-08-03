using KanzApi.Vendors.Oto.Models;

namespace KanzApi.Vendors.Oto.Services;

public interface IOtoAwbPrinterService
{

    OtoAwbPrinterResponse Print(string id);
}
