/**
 * 日期转换扩展, 该扩展用于将前端输入带UTC标识的日期转换成 CST 日期，用来解决前端输入和后端应用日期格式不一致的问题。
 * 
 * 前端: `2021-12-20T02:13:46.633Z`=> 未使用扩展：`2021-12-20T02:13:46.633Z` => 使用扩展后：`2021-12-20T10:13:46.633`
 * */
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Json.SystemTextJson.JsonConverters;

namespace UAP.MvcExtensions.JsonConverters
{
    /// <summary>
    /// JsonConverters 扩展
    /// </summary>
    public static class JsonConvertersExtensions
    {
        /// <summary>
        /// 使用 CST 时间参数转换
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection UseCstDateTimeConverter([NotNull] this IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Transient<AbpDateTimeConverter, UAPDateTimeConverter>());
            services.Replace(ServiceDescriptor.Transient<AbpNullableDateTimeConverter, UAPNullableDateTimeConverter>());
            return services;
        }
    }
}
