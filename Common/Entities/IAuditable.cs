namespace KanzApi.Common.Entities;

public interface IAuditable : ILoggable
{

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }
}
