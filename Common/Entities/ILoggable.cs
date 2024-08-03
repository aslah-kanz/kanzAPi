namespace KanzApi.Common.Entities;

public interface ILoggable
{

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }
}
