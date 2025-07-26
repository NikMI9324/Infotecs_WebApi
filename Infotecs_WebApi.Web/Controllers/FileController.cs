using Infotecs_WebApi.Application.Interfaces;
using Infotecs_WebApi.Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Infotecs_WebApi.Application.QueryClass;

namespace Infotecs_WebApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IDataProcessingService _dataProcessingService;
        private readonly IResultQueryService _resultFilteringService;
        private readonly IValueRepository _valueRepository;
        private readonly IResultRepository _resultRepository;

        public FileController(
            IDataProcessingService dataProcessingService,
            IResultQueryService resultFilteringService,
            IValueRepository valueRepository,
            IResultRepository resultRepository)
        {
            _dataProcessingService = dataProcessingService ?? throw new ArgumentNullException(nameof(dataProcessingService), "Data processing service cannot be null");
            _resultFilteringService = resultFilteringService ?? throw new ArgumentNullException(nameof(resultFilteringService), "Result filtering service cannot be null");
            _valueRepository = valueRepository ?? throw new ArgumentNullException(nameof(valueRepository), "Value repository cannot be null");
            _resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository), "Result repository cannot be null");
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                await _dataProcessingService.ParseCsvFileAsync(file);
                return Ok(new { Message = $"Файл {file.FileName} успешно обработан." });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Внутренняя ошибка сервера.", Details = ex.Message });
            }
        }
        [HttpGet("filter-results")]
        public async Task<IActionResult> FilterResults([FromQuery] ResultFilterQuery filterQuery)
        {
            try
            {
                var results = await _resultFilteringService.GetFilteredDataAsync(filterQuery);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Внутренняя ошибка сервера.", Details = ex.Message });
            }
        }
        [HttpGet("values/{fileName}")]
        public async Task<IActionResult> GetValuesByFileName([FromRoute] string fileName)
        {
            try
            {
                var values = await _valueRepository.GetValuesByFileNameAsync(fileName);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Внутренняя ошибка сервера.", Details = ex.Message });
            }
        }
    }
}
