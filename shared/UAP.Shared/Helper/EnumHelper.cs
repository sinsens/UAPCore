using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UAP.Shared.Helper
{
    public static class EnumHelper
    {
        private static readonly ConcurrentDictionary<Enum, string> EnumDescCache;
        private static readonly ConcurrentDictionary<string, IEnumerable<KeyValuePair<int, string>>> CacheDict;
        private static readonly Lazy<ConcurrentDictionary<string, IEnumerable<KeyValuePair<int, string>>>> DescriptionCacheDict;
        private static readonly Lazy<IEnumerable<Type>> TypeCache;

        static EnumHelper()
        {
            EnumDescCache =
                new ConcurrentDictionary<Enum, string>();
            CacheDict = new ConcurrentDictionary<string, IEnumerable<KeyValuePair<int, string>>>();

            TypeCache = new Lazy<IEnumerable<Type>>(() =>
            {
                var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

                var loadedEnumTypes = loadedAssemblies.Where(x => x.FullName.Contains("LTCI.") || x.FullName.Contains("Volo.Abp."))
                    .SelectMany(x => x.GetTypes()).Where(x => x.IsEnum).ToList();

                // 搜索目录并自动加载 Enum 类型
                foreach (var item in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "LTCI*.dll", SearchOption.AllDirectories))
                {
                    try
                    {
                        var file = new FileInfo(item);
                        if (string.Equals(file.Extension, ".dll", StringComparison.OrdinalIgnoreCase))
                        {
                            var tempAssembly = Assembly.LoadFile(file.FullName);
                            if (loadedAssemblies.Contains(tempAssembly))
                            {
                                continue;
                            }
                            else
                            {
                                var enumTypes = tempAssembly.GetTypes().Where(x => x.IsEnum && loadedEnumTypes.Contains(x) == false).ToList();
                                if (enumTypes.Any())
                                {
                                    loadedEnumTypes.AddRange(enumTypes);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {

                    }
                }
                
                return loadedEnumTypes;
            });

            DescriptionCacheDict = new Lazy<ConcurrentDictionary<string, IEnumerable<KeyValuePair<int, string>>>>(()=> {
                var dict = new ConcurrentDictionary<string, IEnumerable<KeyValuePair<int, string>>>();
                // 注册所有带描述的 Enum 类型
                foreach (var type in TypeCache.Value)
                {
                    foreach (var e in Enum.GetValues(type))
                    {
                        var field = type.GetField(e.ToString());
                        var objs = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (objs != null && objs.Length > 0)
                        {
                            dict.GetOrAdd(type.Name, type.ToDictionary());
                            break;
                        }
                    }
                }
                return dict;
            });
        }

        public static string GetEnumDescription(this Enum enumValue)
        {
            if (enumValue == null)
            {
                return string.Empty;
            }

            return EnumDescCache.GetOrAdd(enumValue, e =>
            {
                var str = e.ToString();
                var field = e.GetType().GetField(str);
                var objs = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length <= 0)
                {
                    return str;
                }

                var da = (DescriptionAttribute)objs[0];
                return da.Description;
            });
        }

        public static IEnumerable<KeyValuePair<int, string>> GetEnumDicByTypeName(string enumTypeName)
        {
            if (string.IsNullOrWhiteSpace(enumTypeName))
            {
                return Enumerable.Empty<KeyValuePair<int, string>>();
            }
            return CacheDict.GetOrAdd(enumTypeName, typeName =>
            {
                var enumType =
                    TypeCache.Value.FirstOrDefault(x =>
                        x.Name.Equals(enumTypeName, StringComparison.OrdinalIgnoreCase));
                return enumType.ToDictionary();
            });
        }

        public static IEnumerable<KeyValuePair<int, string>> ToDictionary(this Type enumType)
        {
            if (enumType == null)
            {
                return Enumerable.Empty<KeyValuePair<int, string>>();
            }

            var result = new List<KeyValuePair<int, KeyValuePair<int, string>>>();

            foreach (var e in Enum.GetValues(enumType))
            {
                var field = enumType.GetField(e.ToString());
                var intVal = (int)e;
                var order = intVal;

                var objs = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length <= 0)
                {
                    continue;
                }

                var da = (DescriptionAttribute)objs[0];
                result.Add(new KeyValuePair<int, KeyValuePair<int, string>>(order, new KeyValuePair<int, string>(intVal, da.Description)));
            }

            return result.OrderBy(t => t.Key).Select(t => t.Value).ToList();
        }

        public static IDictionary<string, IEnumerable<KeyValuePair<int, string>>> GetAllDescriptionEnums()
        {
            return DescriptionCacheDict.Value;
        }

        /// <summary>
        /// 拆分枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public static List<T> SplitEnum<T>(this T e) where T : Enum
        {
            var result = new List<T>();
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                if ((Convert.ToInt32(item) & Convert.ToInt32(e)) > 0)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 合并枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eArray"></param>
        /// <returns></returns>
        public static T MergeEnum<T>(this IEnumerable<T> eArray) where T : Enum
        {
            var enumValue = 0;
            foreach (T item in eArray)
            {
                enumValue |= Convert.ToInt32(item);
            }
            //e.ForEach(p => enumValue |= Convert.ToInt32(p));
            return (T)Enum.ToObject(typeof(T), enumValue);
        }

        /// <summary>
        /// 枚举是否包含另外一个枚举(位运算)
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool IsContains(this Enum e,Enum subEnum) 
        {
            return (Convert.ToInt32(e) & Convert.ToInt32(subEnum)) > 0;
        }
    }
}