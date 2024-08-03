using System.Linq.Expressions;
using System.Text.RegularExpressions;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KanzApi.Common.Services;

public abstract partial class CrudService<T, ID>(ICrudRepository<T, ID> repository)
: ICrudService<T, ID> where T : CommonEntity<ID>
{

    protected readonly ICrudRepository<T, ID> _repository = repository;

    [GeneratedRegex("\\((.*)\\)")]
    private static partial Regex BracketedValueRegex();

    private void ValidateErrorUpdate(DbUpdateException e)
    {
        if (e.InnerException is SqlException se)
        {
            foreach (SqlError error in se.Errors)
            {
                if (error.Number == 2601 || error.Number == 2627)
                {
                    Regex regex = BracketedValueRegex();
                    Match match = regex.Match(error.Message);
                    throw new UniqueConstraintViolationException(match.Groups[1].Value);
                }
            }
        }
    }

    public virtual T Add(T entity)
    {
        try
        {
            entity = _repository.Add(entity);
            _repository.Commit();

            return entity;
        }
        catch (DbUpdateException e)
        {
            ValidateErrorUpdate(e);

            throw;
        }
    }

    public virtual T Edit(T entity)
    {
        try
        {
            entity = _repository.Edit(entity);
            _repository.Commit();

            return entity;
        }
        catch (DbUpdateException e)
        {
            ValidateErrorUpdate(e);

            throw;
        }
    }

    public virtual T Remove(T entity)
    {
        entity = _repository.Remove(entity);
        _repository.Commit();

        return entity;
    }

    public T RemoveById(ID id)
    {
        T entity = GetById(id);
        return Remove(entity);
    }

    public virtual int RemoveAllByPredicate(Expression<Func<T, bool>> predicate)
    {
        return _repository.RemoveAllByPredicate(predicate);
    }

    public virtual T? FindById(ID id, Expression<Func<T, bool>>? predicate)
    {
        return _repository.FindById(id, predicate);
    }

    public T? FindById(ID id)
    {
        return FindById(id, null);
    }

    public virtual T? FindByPredicate(Expression<Func<T, bool>> predicate)
    {
        return _repository.FindByPredicate(predicate);
    }

    public virtual T GetById(ID id)
    {
        return FindById(id) ?? throw EntityNotFoundException.From(typeof(T), id);
    }

    public virtual PaginatedEntity<T> FindAll(Page page, Expression<Func<T, bool>>? predicate, Sort? sort)
    {
        return _repository.FindAll(page, predicate, sort);
    }

    public PaginatedEntity<T> FindAll(IPageableParam<T> param)
    {
        // prevent if page less than 1 is inputted, keep set the page to 1 if that happen
        var activePage = (param.Page < 1) ? 1 : param.Page;

        Page page = new(activePage, param.Size);
        return FindAll(page, param.ToPredicate(), Sort.From(param));
    }

    public virtual IEnumerable<T> FindAll(Expression<Func<T, bool>>? predicate, Sort? sort)
    {
        return _repository.FindAll(predicate, sort);
    }

    public IEnumerable<T> FindAll(ISearchableParam<T> param)
    {
        return FindAll(param.ToPredicate(), Sort.From(param));
    }

    public IEnumerable<T> FindAll(ISortableParam param)
    {
        return FindAll(null, Sort.From(param));
    }

    public IEnumerable<T> FindAll()
    {
        return FindAll(null, null);
    }
}
