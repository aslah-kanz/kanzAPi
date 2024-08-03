namespace KanzApi.Common.Models;

public class PaginatedEntity<T>(IEnumerable<T> content, int page, int size, int count)
{

    private IEnumerable<T> _content = content;

    public IEnumerable<T> Content { get { return _content; } set { _content = value; } }

    private int _page = page;
    public int Page { get { return _page; } set { _page = value; } }

    private int _size = size;
    public int Size { get { return _size; } set { _size = value; } }

    private int _count = count;
    public int Count { get { return _count; } set { _count = value; } }
}
