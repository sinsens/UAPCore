using UAP.Shared.Helper;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Text.Json;
using Volo.Abp.Json;
using Volo.Abp.Json.SystemTextJson.JsonConverters;
using Volo.Abp.Timing;

namespace UAP.MvcExtensions.JsonConverters
{
    public class UAPDateTimeConverter : AbpDateTimeConverter
    {
        private readonly IClock _clock;
        private readonly AbpJsonOptions _options;

        public UAPDateTimeConverter(IClock clock, IOptions<AbpJsonOptions> abpJsonOptions) : base(clock, abpJsonOptions)
        {
            _clock = clock;
            _options = abpJsonOptions.Value;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!_options.DefaultDateTimeFormat.IsNullOrWhiteSpace())
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    var s = reader.GetString();
                    if (DateTime.TryParseExact(s, _options.DefaultDateTimeFormat, CultureInfo.CurrentUICulture, DateTimeStyles.None, out var d1))
                    {
                        return _clock.Normalize(d1.ToCstTime());
                    }

                    throw new JsonException($"'{s}' can't parse to DateTime({_options.DefaultDateTimeFormat})!");
                }

                throw new JsonException("Reader's TokenType is not String!");
            }

            if (reader.TryGetDateTime(out var d2))
            {
                return _clock.Normalize(d2.ToCstTime());
            }

            throw new JsonException("Can't get datetime from the reader!");
        }
    }
}
