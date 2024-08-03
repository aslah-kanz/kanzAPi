using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KanzApi.Common.Entities;
using KanzApi.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Common.Models.Param;

public abstract class PageableParam<E, T> : SearchableParam<E, T>, IPageableParam<E, T>
where E : Enum
where T : CommonEntity
{

    private int _page;

    [SwaggerParameter("<i>Page index</i>")]
    [DefaultValue(0)]
    [Range(0, Int32.MaxValue, ErrorMessageResourceName = "Min", ErrorMessageResourceType = typeof(ValidationMessages))]
    public int Page { get { return _page; } set { _page = value; } }

    private int _size = 10;

    [SwaggerParameter("<i>Page size</i>")]
    [DefaultValue(10)]
    [Range(0, 100, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(ValidationMessages))]
    public int Size { get { return _size; } set { _size = value; } }

    public PageableParam(E sort) : base(sort) { }

    public PageableParam(E sort, EOrder order) : base(sort, order) { }
}
