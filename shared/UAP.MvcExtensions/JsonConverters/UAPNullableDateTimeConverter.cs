using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Text.Json;
using UAP.Shared.Helper;
using Volo.Abp.Json;
using Volo.Abp.Json.SystemTextJson.JsonConverters;
using Volo.Abp.Timing;

namespace UAP.MvcExtensions.JsonConverters
{
    public class UAPNullableDateTimeConverter : AbpNullableDateTimeConverter
    {
        private readonly IClock _clock;
        private readonly AbpJsonOptions _options;

        public UAPNullableDateTimeConverter(IClock clock, IOptions<AbpJsonOptions> abpJsonOptions) : base(clock, abpJsonOptions)
        {
            _clock = clock;
            _options = abpJsonOptions.Value;
        }

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!_options.DefaultDateTimeFormat.IsNullOrWhiteSpace())
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    string @string = reader.GetString();
                    if (DateTime.TryParseExact(@string, _options.DefaultDateTimeFormat, CultureInfo.CurrentUICulture, DateTimeStyles.None, out DateTime result))
                    {
                        return _clock.Normalize(result.ToCstTime());
                    }

                    throw new JsonException("'" + @string + "' can't parse to DateTime(" + _options.DefaultDateTimeFormat + ")!");
                }

                throw new JsonException("Reader's TokenType is not String!");
            }

            if (reader.TryGetDateTime(out DateTime value))
            {
                return _clock.Normalize(value.ToCstTime());
            }

            return null;
        }
    }
}
