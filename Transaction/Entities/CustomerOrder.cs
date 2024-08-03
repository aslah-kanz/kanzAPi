using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using KanzApi.Transaction.Models;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Entities;

[Table("CustomerOrder", Schema = "Transaction")]
public class CustomerOrder : CommonEntity<Guid?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid? Id { get; set; }

    [MaxLength(100)]
    public string? Code { get; set; }

    [MaxLength(100)]
    public string? InvoiceNumber { get; set; }

    [MaxLength(30)]
    public string? PaymentTrackId { get; set; }

    [MaxLength(30)]
    public string? PaymentTransactionId { get; set; }

    private int? _principalId;

    [Required]
    public int? PrincipalId { get { return _principalId; } set { _principalId = value; } }

    private Principal? _principal;

    public virtual Principal? Principal
    { get { return _principal; } set { _principalId = value?.Id; _principal = value; } }

    private int? _principalDetailId;

    public int? PrincipalDetailId { get { return _principalDetailId; } set { _principalDetailId = value; } }

    private PrincipalDetail? _principalDetail;

    public virtual PrincipalDetail? PrincipalDetail
    { get { return _principalDetail; } set { _principalDetailId = value?.Id; _principalDetail = value; } }

    private int? _addressId;

    [Required]
    public int? AddressId { get { return _addressId; } set { _addressId = value; } }

    private PrincipalAddress? _address;

    public virtual PrincipalAddress? Address
    { get { return _address; } set { _addressId = value?.Id; _address = value; } }

    private int? _paymentMethodId;

    public int? PaymentMethodId { get { return _paymentMethodId; } set { _paymentMethodId = value; } }

    private PaymentMethod? _paymentMethod;

    public virtual PaymentMethod? PaymentMethod
    { get { return _paymentMethod; } set { _paymentMethodId = value?.Id; _paymentMethod = value; } }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? SubTotal { get; set; } = 0;

    [MaxLength(100)]
    public string? PromoCode { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DiscountPrice { get; set; } = 0;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? GrandTotal { get; set; } = 0;

    private ECustomerOrderStatus? _status = ECustomerOrderStatus.Open;

    [Required]
    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public ECustomerOrderStatus? Status { get { return _status; } set { _status = value; } }

    private int? _higlightedProductId;

    [Required]
    public int? HiglightedProductId { get { return _higlightedProductId; } set { _higlightedProductId = value; } }

    private ProductEntity? _higlightedProduct;

    public virtual ProductEntity? HiglightedProduct
    {
        get { return _higlightedProduct; }
        set
        {
            if (value != null) _higlightedProductId = value.Id;
            _higlightedProduct = value;
        }
    }

    private int? _deliveryOptionId;

    public int? DeliveryOptionId { get { return _deliveryOptionId; } set { _deliveryOptionId = value; } }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? EstimatedDeliveryCost { get; set; } = 0;

    private List<DeliveryOption>? _deliveryOptions = [];

    [Required]
    public List<DeliveryOption>? DeliveryOptions { get { return _deliveryOptions; } set { _deliveryOptions = value; } }

    public DeliveryOption? DeliveryOption
    {
        get
        {
            return _deliveryOptions?.FirstOrDefault(e => e.Id == _deliveryOptionId);
        }
    }

    [MaxLength(255)]
    public string? Comment { get; set; }

    public bool Cancelable { get { return _status == ECustomerOrderStatus.Paid; } }

    public virtual List<CustomerOrderItem> Items { get; set; } = [];

    public virtual List<StoreOrder> StoreOrders { get; set; } = [];

    public static Expression<Func<CustomerOrder, bool>> QPaymentTrackIdEquals(string value)
    {
        return arg => value.Equals(arg.PaymentTrackId);
    }

    public static Expression<Func<CustomerOrder, bool>> QInvoiceNumberContains(string value)
    {
        return arg => arg.InvoiceNumber != null && arg.InvoiceNumber.Contains(value);
    }

    public static Expression<Func<CustomerOrder, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }

    public static Expression<Func<CustomerOrder, bool>> QPrincipalDetailIdEquals(int value)
    {
        return arg => arg.PrincipalDetailId == value;
    }

    public static Expression<Func<CustomerOrder, bool>> QStatusEquals(ECustomerOrderStatus value)
    {
        return arg => arg.Status == value;
    }

    public static Expression<Func<CustomerOrder, bool>> QStatusContains(params ECustomerOrderStatus[] value)
    {
        return arg => value.Any(p => p == arg.Status);
    }

    public static Expression<Func<CustomerOrder, bool>> QStoreOrderStoreIdsEquals(ISet<int> value)
    {
        return arg => arg.StoreOrders.Any(p => value.Contains((int)p.StoreId!));
    }

    public static Expression<Func<CustomerOrder, bool>> QStatusNotContains(List<ECustomerOrderStatus> value)
    {
        return arg => !value.Contains((ECustomerOrderStatus)arg.Status!);
    }
}
