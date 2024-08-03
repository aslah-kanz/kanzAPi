using System.Linq.Expressions;
using KanzApi.Account.Entities;
using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Account.Models.Param;

public class PrincipalPageableParam : PageableParam<EPrincipalSort, Principal>
{

    private EPrincipalType? _type;

    public EPrincipalType? Type { get { return _type; } set { _type = value; } }

    private EPrincipalStatus? _status;

    public EPrincipalStatus? Status { get { return _status; } set { _status = value; } }

    private string? _userName;

    [SwaggerParameter("<i>Contain</i> : userName")]
    public string? UserName { get { return _userName; } set { _userName = value; } }

    private string? _firstName;

    [SwaggerParameter("<i>Contain</i> : firstName")]
    public string? FirstName { get { return _firstName; } set { _firstName = value; } }

    private string? _lastName;

    [SwaggerParameter("<i>Contain</i> : lastName")]
    public string? LastName { get { return _lastName; } set { _lastName = value; } }

    private string? _email;

    [SwaggerParameter("<i>Contain</i> : email")]
    public string? Email { get { return _email; } set { _email = value; } }

    private EGender? _gender;

    public EGender? Gender { get { return _gender; } set { _gender = value; } }

    private string? _phoneNumber;

    [SwaggerParameter("<i>Starts With</i> : phoneNumber")]
    public string? PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }

    [SwaggerParameter("<i>Contains</i> : userName, firstName, lastName, email")]
    public override string? Search { get; set; }

    public PrincipalPageableParam() : base(EPrincipalSort.UpdatedAt) { }

    protected override Expression<Func<Principal, bool>> ToSearchPredicate(string search)
    {
        return Principal.QFirstNameContains(search).Or(
            Principal.QLastNameContains(search), Principal.QUsernameContains(search)
            , Principal.QEmailContains(search));
    }

    public override Expression<Func<Principal, bool>> ToPredicate()
    {
        Expression<Func<Principal, bool>> result = base.ToPredicate();

        if (_type != null)
        {
            result = result.And(Principal.QTypeEquals((EPrincipalType)_type));
        }
        if (_status != null)
        {
            result = result.And(Principal.QStatusEquals((EPrincipalStatus)_status));
        }
        if (_userName != null)
        {
            result = result.And(Principal.QUsernameContains(_userName));
        }
        if (_firstName != null)
        {
            result = result.And(Principal.QFirstNameContains(_firstName));
        }
        if (_lastName != null)
        {
            result = result.And(Principal.QLastNameContains(_lastName));
        }
        if (_email != null)
        {
            result = result.And(Principal.QEmailContains(_email));
        }
        if (_gender != null)
        {
            result = result.And(Principal.QGenderEquals((EGender)_gender));
        }
        if (_phoneNumber != null)
        {
            result = result.And(Principal.QPhoneNumberStartsWith(_phoneNumber));
        }

        return result;
    }
}
