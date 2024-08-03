namespace KanzApi.Common.Models;

public class Page
{

    private int _index;
    public int Index { get { return _index; } set { _index = value; } }

    private int _size = 10;
    public int Size { get { return _size; } set { _size = value; } }

    public Page(int index)
    {
        _index = index;
    }

    public Page(int index, int size)
    {
        _index = index;
        _size = size;
    }
}
