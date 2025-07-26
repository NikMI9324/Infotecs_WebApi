using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs_WebApi.Application.CsvValidation
{
    internal record CsvRecord
    {
        [DateRange("2000-01-01T00-00-00.0000Z")]
        public DateTime Date { get; init; }
        [Range(0, int.MaxValue, ErrorMessage = "Значение вермени выполнения не должно быть отрицательным")]
        public int ExecutionTime { get; init; }
        [Range(0, double.MaxValue, ErrorMessage = "Значение показателя не должно быть отрицательным")]
        public double ValueDefinition { get; init; }
        public CsvRecord(DateTime date, int executionTime, double valueDefinition)
        {
            Date = date;
            ExecutionTime = executionTime;
            ValueDefinition = valueDefinition;
        }
    }
}
