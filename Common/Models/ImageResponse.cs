using KanzApi.Common.Entities;

namespace KanzApi.Common.Models;

public class ImageResponse
{

    public long Id { get; set; }

    public EImageGroup? Group { get; set; }

    public string Name { get; set; } = "";

    public string Url { get; set; } = "";

    public int Width { get; set; }

    public int Height { get; set; }

    public string Type { get; set; } = "";
}
