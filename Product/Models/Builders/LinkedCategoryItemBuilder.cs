using KanzApi.Extensions;
using KanzApi.Product.Entities;
using KanzApi.Product.Services;
using MapsterMapper;

namespace KanzApi.Product.Models.Builders;

public class LinkedCategoryItemBuilder(IMapper mapper, IEnumerable<Category> entities)
{

    private readonly IMapper _mapper = mapper;
    private readonly IEnumerable<Category> _entities = entities;
    private readonly Dictionary<int, LinkedCategoryItem> _modelMap = [];
    private CategoryService? _service;

    public LinkedCategoryItemBuilder SetService(CategoryService service)
    {
        _service = service;
        return this;
    }

    private LinkedCategoryItem MapParent(Category entity, ISet<LinkedCategoryItem> models)
    {
        int id = (int)entity.Id!;

        if (_modelMap.TryGetValue(id, out LinkedCategoryItem? value))
        {
            return value;
        }

        LinkedCategoryItem model = _mapper.Map<LinkedCategoryItem>(entity);
        _modelMap[id] = model;

        if (entity.ParentId != null)
        {
            Category parentEntity = _entities.Find(e => e.Id == entity.ParentId) ?? entity.Parent!;
            LinkedCategoryItem parentModel = MapParent(parentEntity, models);
            parentModel.Items.Add(model);
        }
        else
        {
            models.Add(model);
        }

        return model;
    }

    private void MapChildren(LinkedCategoryItem? parent, Category entity)
    {
        int id = (int)entity.Id!;

        LinkedCategoryItem model;
        if (!_modelMap.TryGetValue(id, out LinkedCategoryItem? value))
        {
            model = _mapper.Map<LinkedCategoryItem>(entity);
            _modelMap[id] = model;

            parent?.Items.Add(model);
        }
        else
        {
            model = value;
        }

        if (_service != null && _entities.Any(e => e.Id == entity.Id))
        {
            IEnumerable<Category> children = _service.FindAll(Category.QParentEquals(id), null);
            foreach (Category ce in children)
            {
                MapChildren(model, ce);
            }
        }
    }

    public IEnumerable<LinkedCategoryItem> Build()
    {
        ISet<LinkedCategoryItem> models = new SortedSet<LinkedCategoryItem>();
        foreach (Category entity in _entities)
        {
            MapParent(entity, models);
            MapChildren(null, entity);
        }

        return models;
    }
}
