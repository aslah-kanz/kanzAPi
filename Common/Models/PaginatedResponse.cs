namespace KanzApi.Common.Models;

public class PaginatedResponse<T>
{

    private IEnumerable<T> _content = [];

    public IEnumerable<T> Content
    {
        get { return _content; }
        set { _content = value; _count = value.Count(); }
    }

    public int Page { get; set; }

    public int Size { get; set; }

    private int _count;
    public int Count { get { return _count; } set { _count = value; } }

    public int TotalCount { get; set; }

    public static PaginatedResponse<T> From<U>(PaginatedEntity<U> pEntity, IEnumerable<T> content)
    {
        return new()
        {
            Content = content,
            Page = pEntity.Page,
            Size = pEntity.Size,
            TotalCount = pEntity.Count
        };
    }

    public static PaginatedResponse<T> From(PaginatedEntity<T> pEntity)
    {
        return From(pEntity, pEntity.Content);
    }
}
