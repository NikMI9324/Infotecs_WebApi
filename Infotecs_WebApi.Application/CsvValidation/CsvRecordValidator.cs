using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs_WebApi.Application.CsvValidation
{
    internal static class CsvRecordValidator
    {
        private const string DateFormat = "yyyy-MM-dd'T'HH-mm-ss.ffff'Z'";
        public static ValidationResult Validate(string[] parts, out DateTime date, out int executionTime, out double valueDefinition)
        {
            date = default;
            executionTime = default;
            valueDefinition = default;

            if (parts == null || parts.Length != 3)
            {
                return new ValidationResult("Строка отсутствует или неверный формат строки: " +
                                         string.Join(";", parts ?? Array.Empty<string>()));
            }

            if (!DateTime.TryParseExact(parts[0], DateFormat, CultureInfo.InvariantCulture,
                                      DateTimeStyles.AssumeUniversal, out date))
                return new ValidationResult("Неверный формат даты: " + parts[0]);

            if (!int.TryParse(parts[1], out executionTime))
                return new ValidationResult("Неверный формат времени выполнения: " + parts[1]);

            if (!double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out valueDefinition))
                return new ValidationResult("Неверный формат значения показателя: " + parts[2]);

            var csvRecord = new CsvRecord(date, executionTime, valueDefinition);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(csvRecord, new ValidationContext(csvRecord), validationResults, true))
            {
                return new ValidationResult("Ошибка валидации: " +
                                          string.Join(", ", validationResults.Select(r => r.ErrorMessage)));
            }

            return ValidationResult.Success;
        }
        public static CsvRecord GetValidatedValues(string[] parts)
        {
            var result = Validate(parts, out var date, out var executionTime, out var valueDefinition);

            if (result != ValidationResult.Success)
                throw new ValidationException(result.ErrorMessage);

            

            return new CsvRecord(date, executionTime, valueDefinition);
        }
    }
}
