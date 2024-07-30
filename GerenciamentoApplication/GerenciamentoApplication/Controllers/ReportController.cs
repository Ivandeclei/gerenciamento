using AutoMapper;
using Gerenciamento.Domain.Models;
using Gerenciamento.Domain.Services;
using GerenciamentoApplication.Constants;
using GerenciamentoApplication.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoApplication.Controllers
{
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        public ReportController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route(UriTemplates.REPORT_LAST_30_DAYS)]
        public async Task<IActionResult> GetTaskByUserAsync([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var report = await _reportService.GetTaskByUserAsync(user);
            var result = _mapper.Map<IEnumerable<ReportResultDto>>(report);

            return Ok(result);
        }



    }
}
