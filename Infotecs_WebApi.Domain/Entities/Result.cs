namespace Infotecs_WebApi.Domain.Entities
{
    public class Result
    {
        public string? FileName { get; set; }
        public double TimeDeltaSeconds { get; set; }
        public DateTime MinimalDate { get; set; }
        public double AverageExecutionTime { get; set; }
        public double AverageValueDefinition { get; set; }
        public double MedianValueDefinition { get; set; }
        public double MaxValueDefinition { get; set; }
        public double MinValueDefinition { get; set; }

    }
}
