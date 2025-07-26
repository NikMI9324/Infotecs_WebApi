using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infotecs_WebApi.Application.QueryClass
{
    public class ResultFilterQuery
    {
        private const string DateFormat = "yyyy-MM-dd'T'HH-mm-ss.ffff'Z'";
        private const string DateFormatDisplay = "ГГГГ-ММ-ДДTчч-мм-сс.ммммZ";
        public string? FileName { get; set; }
        public string? StartTimeFromString { get; set; }
        public string? StartTimeToString { get; set; }
        [SwaggerIgnore]
        public DateTime? StartTimeFrom => ParseDate(StartTimeFromString);
        [SwaggerIgnore]
        public DateTime? StartTimeTo => ParseDate(StartTimeToString);
        public double? AverageValueFrom { get; set; }
        public double? AverageValueTo { get; set; }
        public double? AverageExecutionTimeFrom { get; set; }
        public double? AverageExecutionTimeTo { get; set; }

        private DateTime? ParseDate(string? dateString)
        {
            if (string.IsNullOrEmpty(dateString))
                return null;
            if (DateTime.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var parsedDate))
            {
                return parsedDate;
            }
            throw new FormatException($"Дата должна быть в формате {DateFormatDisplay}");
        }
    }
}
