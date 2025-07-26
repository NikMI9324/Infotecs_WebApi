using System.ComponentModel.DataAnnotations;

namespace Infotecs_WebApi.Domain.Entities
{
    public class Value
    {
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public DateTime Date { get; set; }
        public int ExecutionTime { get; set; }
        public double ValueDefinition { get; set; }
    }
}
