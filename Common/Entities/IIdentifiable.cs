namespace KanzApi.Common.Entities;

public interface IIdentifiable
{

    object? Id { get; }
}

public interface IIdentifiable<T> : IIdentifiable
{

    new T Id { get; }
}
