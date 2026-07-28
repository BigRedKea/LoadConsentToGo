using System.Collections.Concurrent;
using System.Data.Common;
using System.Reflection;

namespace LoadConsentToGo
{
    internal static class DbMapper
    {
        // Cache by type full name + schema signature
        static ConcurrentDictionary<string, PropertyMap[]> _mapCache = new();

        record PropertyMap(int Ordinal, PropertyInfo Prop);

        public static T MapRowTo<T>(DbDataReader reader) where T : new()
        {
            var names = new string[reader.FieldCount];
            for (int i = 0; i < reader.FieldCount; i++)
                names[i] = reader.GetName(i).Trim();

            var signature = string.Join("|", names);
            var cacheKey = typeof(T).FullName + "|" + signature;

            if (!_mapCache.TryGetValue(cacheKey, out var maps))
            {
                var nameToIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < names.Length; i++)
                    nameToIndex[names[i]] = i;

                var props = new List<PropertyMap>();
                foreach (var p in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var attr = p.GetCustomAttribute<ColumnNameAttribute>();
                    var col = (attr?.Name ?? p.Name).Trim();
                    if (nameToIndex.TryGetValue(col, out var idx))
                        props.Add(new PropertyMap(idx, p));
                }

                maps = props.ToArray();
                _mapCache[cacheKey] = maps;
            }

            var obj = new T();
            foreach (var m in maps)
            {
                if (reader.IsDBNull(m.Ordinal)) continue;
                var raw = reader.GetValue(m.Ordinal);
                var targetType = Nullable.GetUnderlyingType(m.Prop.PropertyType) ?? m.Prop.PropertyType;
                try
                {
                    var value = Convert.ChangeType(raw, targetType);
                    m.Prop.SetValue(obj, value);
                }
                catch
                {
                    // ignore conversion errors for now
                }
            }

            return obj;
        }
    }
}
