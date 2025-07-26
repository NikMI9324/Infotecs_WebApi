using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Infotecs_WebApi.Application.CsvValidation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DateRangeAttribute : ValidationAttribute
    {
        private readonly DateTime _minDate;
        private const string DateFormat = "yyyy-MM-dd'T'HH-mm-ss.ffff'Z'";
        private const string DateFormatDisplay = "ГГГГ-ММ-ДДTчч-мм-сс.ммммZ";
        public DateRangeAttribute(string minDate)
        {
            if (!DateTime.TryParseExact(minDate, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out _minDate))
                throw new ArgumentException($"Минимальная дата должна быть в формате {DateFormatDisplay}");
        }
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime dateTime)
                return new ValidationResult(ErrorMessage ?? $"Значение должно быть строкой в формате {DateFormatDisplay}");

            if (dateTime < _minDate)
                return new ValidationResult(ErrorMessage 
                    ?? $"Дата должна быть не раньше {_minDate.ToString(DateFormat, CultureInfo.InvariantCulture)}");
            if(dateTime > DateTime.UtcNow)
                return new ValidationResult(ErrorMessage 
                    ?? "Дата не может быть в будущем");
            return ValidationResult.Success;
        }

    }
}
