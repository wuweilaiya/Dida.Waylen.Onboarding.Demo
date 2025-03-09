using System.Collections.Concurrent;
using System.ComponentModel;
using Core.Plugin.Extensions.Common;
using Core.Plugin.Web.Sidecar.BasicData.Interfaces;
using Dolphin.Shared.Model.Enums;
using Framework.Common.EnumOperation;

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Infrastructure;

public class BasicDataProvider : IBasicDataProvider, ISingleton
{
    private Dictionary<string, List<dynamic>>? _keyValuePairs;
    private object _lock = new object();

    public dynamic GetDatas()
    {
        ConcurrentDictionary<string, dynamic> result = new ConcurrentDictionary<string, dynamic>();

        return result.ToDictionary(x => x.Key, x => x.Value);
    }

    public dynamic GetKeyValuePairs()
    {
        if (_keyValuePairs is null)
        {
            lock (_lock)
            {
                if (_keyValuePairs is null)
                {
                    _keyValuePairs = new();
                    var enumMap = GetEnumMap();
                    _keyValuePairs.AddRange(enumMap, true);
                }
            }
        }

        return _keyValuePairs;
    }

    private Dictionary<string, List<dynamic>> GetEnumMap()
    {
        var result = new ConcurrentDictionary<string, List<dynamic>>();
        AddEnum<CollectionFieldType>();
        AddEnum<TextCollectionFieldType>();
        AddEnum<HotelStarRatingEnum>();
        AddEnum<RoomTypeEnum>();
        AddEnum<BedTypeEnum>();

        return result.ToDictionary(x => x.Key, x => x.Value);

        void AddEnum<T>(string? key = null) where T : struct
        {
            key ??= typeof(T).Name;
            var enumMap = EnumHelper.GetDictionary<T, DescriptionAttribute>((attr) => attr.Description)
                                    .Select(s => (dynamic)new { id = Convert.ToInt32(s.Key), name = s.Value.ToString() })
                                    .OrderBy(s => s.id)
                                    .ToList();
            result.GetOrAdd(JsonNamingPolicy.CamelCase.ConvertName(key), enumMap);
        }
    }
}
