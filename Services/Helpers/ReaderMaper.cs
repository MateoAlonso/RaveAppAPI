using System.Collections;
using System.Data;
using System.Reflection;

namespace RaveAppAPI.Services.Helpers
{
    public static class ReaderMaper
    {
        public static IEnumerable<T> ReaderToObject<T>(IDataReader rd) where T : class, new()
        {
            IEnumerable<T> list = new List<T>();
            Type type = typeof(T);
            var attr = type.GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(ColumnNameAttribute)))
                .ToDictionary(p => ((ColumnNameAttribute)Attribute.GetCustomAttribute(p, typeof(ColumnNameAttribute))).Name, p => p);
            while (rd.Read())
            {
                var t = new T();
                for (int i = 0; i < rd.FieldCount; i++)
                {
                    if (!rd.IsDBNull(i))
                    {
                        string fieldName = rd.GetName(i).ToUpper();

                        if (attr.ContainsKey(fieldName))
                        {
                            PropertyInfo propInfo = attr.GetValueOrDefault(fieldName);
                            object value = rd.GetValue(i);
                            if (string.Equals(propInfo.PropertyType.GetTypeInfo().Name.ToUpper(), "BOOLEAN"))
                            {
                                value = Convert.ToBoolean(value);
                            }
                            propInfo.SetValue(t, value);
                        }
                    }
                }
                yield return t;
            }
        }

        public static IEnumerable<T> ReaderToObjectRecursive<T>(IDataReader rd) where T : class, new()
        {
            while (rd.Read())
            {
                yield return (T)MapObject(typeof(T), rd);
            }
        }
        public static IEnumerable<T> ReaderToSimpleType<T>(IDataReader rd)
        {
            while (rd.Read())
            {
                if (!rd.IsDBNull(0))
                {
                    yield return (T)Convert.ChangeType(rd.GetValue(0), typeof(T));
                }
            }
        }

        private static object MapObject(Type type, IDataReader rd, bool isRecursive = true)
        {
            object instance = Activator.CreateInstance(type);
            var properties = type.GetProperties()
                                 .Where(p => Attribute.IsDefined(p, typeof(ColumnNameAttribute)));
            int ordinal;
            foreach (var prop in properties)
            {
                var attr = (ColumnNameAttribute)Attribute.GetCustomAttribute(prop, typeof(ColumnNameAttribute));
                string columnName = attr.Name.ToUpper();

                try
                {
                    ordinal = rd.GetOrdinal(columnName);
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }

                if (!rd.IsDBNull(ordinal))
                {
                    if (IsSimpleType(prop.PropertyType))
                    {
                        //object value = Convert.ChangeType(rd.GetValue(ordinal), prop.PropertyType);
                        //prop.SetValue(instance, value);
                        object value = rd.GetValue(ordinal);
                        if (string.Equals(prop.PropertyType.GetTypeInfo().Name.ToUpper(), "BOOLEAN"))
                        {
                            value = Convert.ToBoolean(value);
                        }
                        prop.SetValue(instance, value);
                    }
                }
            }
            if (isRecursive)
            {
                var objects = type.GetProperties().Where(p => !IsSimpleType(p.PropertyType));
                foreach (var obj in objects)
                {
                    var props = obj.PropertyType.GetProperties().Where(p => !IsSimpleType(p.PropertyType));
                    bool match = props.Any(p => objects.Any(o => o.PropertyType == p.PropertyType));
                    object nested = MapObject(obj.PropertyType, rd, !match);
                    obj.SetValue(instance, nested);
                }
            }
            return instance;
        }

        private static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive
                || type.IsEnum
                || type.Equals(typeof(string))
                || type.Equals(typeof(decimal))
                || type.Equals(typeof(DateTime))
                || type.Equals(typeof(DateTimeOffset))
                || type.Equals(typeof(Guid))
                || type.IsGenericType
                || typeof(IEnumerable).IsAssignableFrom(type);
        }

    }
}
