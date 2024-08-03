using KanzApi.Vendors.Oto.Models;
using MapsterMapper;
using System.Text.RegularExpressions;

namespace KanzApi.Transaction.Models.Builders;

public partial class DeliveryOptionsBuilder
{

    private readonly IMapper _mapper;

    private readonly List<string> _whitelist;

    [GeneratedRegex("[0-9]+")]
    private static partial Regex NumericsRegex();

    private readonly Dictionary<string, DeliveryOption> _mergedMap = [];

    public DeliveryOptionsBuilder(IMapper mapper, IConfiguration configuration)
    {
        _mapper = mapper;
        _whitelist = configuration.GetSection("Oto:Company:Whitelist").Get<List<string>>() ?? [];
    }

    private DeliveryOption? FindByRange(int min, int max)
    {
        DeliveryOption? result = null;
        foreach (DeliveryOption item in _mergedMap.Values)
        {
            if (item.MinEstimatedDay < min || item.MaxEstimatedDay > max) continue;
            if (result != null
            && result.MaxEstimatedDay - result.MinEstimatedDay
            >= item.MaxEstimatedDay - item.MinEstimatedDay) continue;

            result = item;
        }

        return result;
    }

    private DeliveryOptionItem? FindItemByRange(Dictionary<string, DeliveryOptionItem> map, int min, int max)
    {
        DeliveryOptionItem? result = null;
        foreach (DeliveryOptionItem item in map.Values)
        {
            if (item.MinEstimatedDay < min || item.MaxEstimatedDay > max) continue;
            if (result != null
            && result.MaxEstimatedDay - result.MinEstimatedDay
            >= item.MaxEstimatedDay - item.MinEstimatedDay) continue;

            result = item;
        }

        return result;
    }

    private void Merge(Dictionary<string, DeliveryOptionItem> map, bool initial)
    {
        foreach (KeyValuePair<string, DeliveryOptionItem> pair in map)
        {
            if (initial)
            {
                DeliveryOption option = new()
                {
                    Id = _mergedMap.Count + 1,
                    MinEstimatedDay = pair.Value.MinEstimatedDay,
                    MaxEstimatedDay = pair.Value.MaxEstimatedDay,
                    EstimatedCost = pair.Value.EstimatedCost,
                    Items = [pair.Value]
                };
                _mergedMap[pair.Key] = option;
            }
            else
            {
                DeliveryOption? option = FindByRange(
                    (int)pair.Value.MinEstimatedDay!, (int)pair.Value.MaxEstimatedDay!);
                if (option != null)
                {
                    DeliveryOption newOption = new()
                    {
                        Id = option.Id,
                        MinEstimatedDay = pair.Value.MinEstimatedDay,
                        MaxEstimatedDay = pair.Value.MaxEstimatedDay,
                        EstimatedCost = option.EstimatedCost + pair.Value.EstimatedCost,
                        Items = [.. option.Items, pair.Value]
                    };
                    _mergedMap[pair.Value.MinEstimatedDay + "-" + pair.Value.MaxEstimatedDay] = newOption;
                }
            }
        }
    }

    private void Merge(Dictionary<string, DeliveryOptionItem> map)
    {
        bool initial = _mergedMap.Count == 0;
        List<string> removableKeys = [];

        foreach (KeyValuePair<string, DeliveryOption> pair in _mergedMap)
        {
            map.Remove(pair.Key, out DeliveryOptionItem? item);
            if (item != null)
            {
                pair.Value.EstimatedCost += item.EstimatedCost;
                pair.Value.Items.Add(item);
            }
            else
            {
                item = FindItemByRange(
                    map, (int)pair.Value.MinEstimatedDay!, (int)pair.Value.MaxEstimatedDay!);
                if (item != null)
                {
                    pair.Value.EstimatedCost += item.EstimatedCost;
                    pair.Value.Items.Add(item);
                }
                else
                {
                    removableKeys.Add(pair.Key);
                }
            }
        }

        Merge(map, initial);

        foreach (string key in removableKeys) _mergedMap.Remove(key);
    }

    private void MapDays(DeliveryOptionItem item, string value)
    {
        Regex regex = NumericsRegex();
        MatchCollection matches = regex.Matches(value);
        if (matches.Count > 1)
        {
            item.MinEstimatedDay = Int32.Parse(matches[0].Value);
            item.MaxEstimatedDay = Int32.Parse(matches[1].Value);
        }
        else if (matches.Count == 1)
        {
            item.MinEstimatedDay = item.MaxEstimatedDay = Int32.Parse(matches[0].Value);
        }
        else
        {
            item.MinEstimatedDay = item.MaxEstimatedDay = 1;
        }
    }

    public DeliveryOptionsBuilder Add(DeliveryDetail detail, IEnumerable<OtoDeliveryCompanyResponse> companies)
    {
        Dictionary<string, DeliveryOptionItem> itemMap = [];
        foreach (OtoDeliveryCompanyResponse company in companies)
        {
            if (!"freePickup".Equals(company.PickupDropoff)) continue;
            if (_whitelist.Any() && !_whitelist.Contains(company.DeliveryCompanyName!)) continue;

            string key = company.AverageDeliveryTime!;

            itemMap.TryGetValue(key, out DeliveryOptionItem? value);
            if (value == null || company.Price < value?.EstimatedCost)
            {
                DeliveryOptionItem item = _mapper.Map<DeliveryOptionItem>(detail);
                item.Id = company.DeliveryOptionId;
                item.Name = company.DeliveryOptionName;
                item.ImageUrl = company.Logo;
                item.EstimatedCost = company.Price;

                MapDays(item, key);

                itemMap[key] = item;
            }
        }

        Merge(itemMap);

        return this;
    }

    public IEnumerable<DeliveryOption> Build()
    {
        return _mergedMap.Values
        .OrderBy(e => e.MinEstimatedDay)
        .ThenBy(e => e.MaxEstimatedDay)
        .ThenBy(e => e.EstimatedCost);
    }
}