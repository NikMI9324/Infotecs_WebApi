using Infotecs_WebApi.Application.CsvValidation;
using Infotecs_WebApi.Application.Interfaces;
using Infotecs_WebApi.Domain.Entities;
using Infotecs_WebApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Infotecs_WebApi.Application.Services
{
    public class DataProcessingService : IDataProcessingService
    {
        private readonly IValueRepository _valueRepo;
        private readonly IResultRepository _resultRepo;

        public DataProcessingService(IValueRepository valueRepo, IResultRepository resultRepo)
        {
            _valueRepo = valueRepo;
            _resultRepo = resultRepo;
        }
        public async Task ParseCsvFileAsync(IFormFile file)
        {
            if (file is null || file.Length == 0)
                throw new ValidationException("Файл не предоставлен");
            var values = new List<Value>();
            using var reader = new StreamReader(file.OpenReadStream());
            var lines = new List<string>();
            while (!reader.EndOfStream)
                lines.Add(await reader.ReadLineAsync());

            if (lines.Count < 1 || lines.Count > 10000)
                throw new ValidationException("Количество строк должно быть от 1 до 10 000");

            foreach (string line in lines)
            {
                var parts = line.Split(';');

                var record = CsvRecordValidator.GetValidatedValues(parts);
                values.Add(new Value
                {
                    Date = record.Date,
                    ExecutionTime = record.ExecutionTime,
                    ValueDefinition = record.ValueDefinition,
                    FileName = file.FileName
                });
            }
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _valueRepo.UpsertDataAsync(values,file.FileName);
            var result = new Result
            {
                FileName = file.FileName,
                TimeDeltaSeconds = (values.Max(v => v.Date) - values.Min(v => v.Date)).TotalSeconds,
                AverageExecutionTime = values.Average(v => v.ExecutionTime),
                AverageValueDefinition = values.Average(v => v.ValueDefinition),
                MedianValueDefinition = GetMedian(values.Select(v => v.ValueDefinition)),
                MaxValueDefinition = values.Max(v => v.ValueDefinition),
                MinValueDefinition = values.Min(v => v.ValueDefinition),
                MinimalDate = values.Min(v => v.Date)
            };
            await _resultRepo.AddOrUpdateAsync(result);
            transaction.Complete();

        }
        private static double GetMedian(IEnumerable<double> values)
        {
            var sorted = values.OrderBy(x => x).ToList();
            if (sorted.Count == 0)
                return 0;
            return sorted.Count % 2 == 0 ? (sorted[sorted.Count / 2 - 1]
                + sorted[sorted.Count / 2]) / 2 : sorted[sorted.Count / 2];
        }
    }
}
