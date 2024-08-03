using KanzApi.Account.Models;

namespace KanzApi.Account.Services;

public interface IDashboardService
{
    public VendorDashboardResponse GetVendorDashboard();
}
