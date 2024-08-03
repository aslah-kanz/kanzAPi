namespace KanzApi.Utils;

public sealed class ErrorCode
{

    private readonly string _key;
    public string Key { get { return _key; } }

    private readonly string _value;
    public string Value { get { return _value; } }

    private ErrorCode(string key, string value)
    {
        _key = key;
        _value = value;
    }

    public static ErrorCode UsernameAndPasswordNotFound
    { get { return new ErrorCode("10000", "UsernameAndPasswordNotFound"); } }
    public static ErrorCode UserNotActive
    { get { return new ErrorCode("10001", "UserNotActive"); } }
    public static ErrorCode InvalidPassword
    { get { return new ErrorCode("10002", "InvalidPassword"); } }
    public static ErrorCode UsernameAlreadyExist
    { get { return new ErrorCode("10003", "UsernameAlreadyExist"); } }
    public static ErrorCode Unauthorized
    { get { return new ErrorCode("10100", "Unauthorized"); } }
    public static ErrorCode Forbidden
    { get { return new ErrorCode("10200", "Forbidden"); } }
    public static ErrorCode PrincipalNotAllowed
    { get { return new ErrorCode("10201", "PrincipalNotAllowed"); } }
    public static ErrorCode PrincipalTypeNotAllowed
    { get { return new ErrorCode("10202", "PrincipalTypeNotAllowed"); } }
    public static ErrorCode StateNotAllowed
    { get { return new ErrorCode("10203", "StateNotAllowed"); } }
    public static ErrorCode InvalidToken
    { get { return new ErrorCode("10300", "InvalidToken"); } }
    public static ErrorCode TokenExpired
    { get { return new ErrorCode("10301", "TokenExpired"); } }
    public static ErrorCode InvalidOtp
    { get { return new ErrorCode("10400", "InvalidOtp"); } }
    public static ErrorCode OtpExpired
    { get { return new ErrorCode("10401", "OtpExpired"); } }
    public static ErrorCode OtpMaxAttempt
    { get { return new ErrorCode("10402", "OtpMaxAttempt"); } }
    public static ErrorCode Validation
    { get { return new ErrorCode("20000", "Validation"); } }
    public static ErrorCode PathNotFound
    { get { return new ErrorCode("20001", "PathNotFound"); } }
    public static ErrorCode InvalidStateChange
    { get { return new ErrorCode("20003", "InvalidStateChange"); } }
    public static ErrorCode ProductMismatch
    { get { return new ErrorCode("20004", "ProductMismatch"); } }
    public static ErrorCode DuplicateProductSlug
    { get { return new ErrorCode("20005", "DuplicateProductSlug"); } }
    public static ErrorCode EntityNotFound
    { get { return new ErrorCode("20100", "EntityNotFound"); } }
    public static ErrorCode RecursiveParent
    { get { return new ErrorCode("20101", "RecursiveParent"); } }
    public static ErrorCode FileAlreadyExist
    { get { return new ErrorCode("20102", "FileAlreadyExist"); } }
    public static ErrorCode SaleItemPriceMismatch
    { get { return new ErrorCode("20202", "SaleItemPriceMismatch"); } }
    public static ErrorCode SaleItemOutOfStock
    { get { return new ErrorCode("20203", "SaleItemOutOfStock"); } }
    public static ErrorCode EmptyCart
    { get { return new ErrorCode("20301", "EmptyCart"); } }
    public static ErrorCode InvalidRefundQuantity
    { get { return new ErrorCode("20401", "InvalidRefundQuantity"); } }
    public static ErrorCode RefundAlreadyExist
    { get { return new ErrorCode("20402", "RefundAlreadyExist"); } }
    public static ErrorCode InvalidRefundPurchaseQuoteStatus
    { get { return new ErrorCode("20403", "InvalidRefundPurchaseQuoteStatus"); } }
    public static ErrorCode RefundTimeExpired
    { get { return new ErrorCode("20504", "RefundTimeExpired"); } }
    public static ErrorCode InvalidExchangeQuantity
    { get { return new ErrorCode("20501", "InvalidExchangeQuantity"); } }
    public static ErrorCode ExchangeAlreadyExist
    { get { return new ErrorCode("20502", "ExchangeAlreadyExist"); } }
    public static ErrorCode ExchangeTimeExpired
    { get { return new ErrorCode("20503", "ExchangeTimeExpired"); } }
    public static ErrorCode InvalidExchangePurchaseQuoteStatus
    { get { return new ErrorCode("20504", "InvalidExchangePurchaseQuoteStatus"); } }
    public static ErrorCode InvalidPurchaseQuoteConfirmedQuantity
    { get { return new ErrorCode("20601", "InvalidPurchaseQuoteConfirmedQuantity"); } }
    public static ErrorCode CancelOrderNotAllowed
    { get { return new ErrorCode("20701", "CancelOrderNotAllowed"); } }
    public static ErrorCode DuplicateStore
    { get { return new ErrorCode("20702", "DuplicateStore"); } }
    public static ErrorCode CannotDeleteStore
    { get { return new ErrorCode("20703", "CannotDeleteStore"); } }
    public static ErrorCode ReviewSubmitted
    { get { return new ErrorCode("20801", "ReviewSubmitted"); } }
    public static ErrorCode Unknown
    { get { return new ErrorCode("30000", "Unknown"); } }
    public static ErrorCode Error
    { get { return new ErrorCode("30001", "Error"); } }
    public static ErrorCode DatabaseError
    { get { return new ErrorCode("30100", "DatabaseError"); } }
    public static ErrorCode UniqueConstraintViolation
    { get { return new ErrorCode("30101", "UniqueConstraintViolation"); } }
    public static ErrorCode MailUnknown
    { get { return new ErrorCode("30200", "MailUnknown"); } }
    public static ErrorCode MailError
    { get { return new ErrorCode("30201", "MailError"); } }
    public static ErrorCode OtpUnknown
    { get { return new ErrorCode("30300", "OtpUnknown"); } }
    public static ErrorCode OtpError
    { get { return new ErrorCode("30301", "OtpError"); } }
    public static ErrorCode DeliveryUnknown
    { get { return new ErrorCode("30400", "DeliveryUnknown"); } }
    public static ErrorCode DeliveryError
    { get { return new ErrorCode("30401", "DeliveryError"); } }
    public static ErrorCode PaymentUnknown
    { get { return new ErrorCode("30500", "PaymentUnknown"); } }
    public static ErrorCode PaymentError
    { get { return new ErrorCode("30501", "PaymentError"); } }
    public static ErrorCode PaymentHashMismatch
    { get { return new ErrorCode("30502", "PaymentHashMismatch"); } }
    public static ErrorCode UrlShortenerUnknown
    { get { return new ErrorCode("30600", "UrlShortenerUnknown"); } }
    public static ErrorCode UrlShortenerError
    { get { return new ErrorCode("30601", "UrlShortenerError"); } }

    public override string ToString()
    {
        return _key;
    }
}
