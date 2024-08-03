using KanzApi.Common.Entities;

namespace KanzApi.Common.Services;

public interface IFilterableCrudService<T, ID> : ICrudService<T, ID> where T : CommonEntity<ID> { }
