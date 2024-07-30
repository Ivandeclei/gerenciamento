using AutoMapper;
using Gerenciamento.Application;
using Gerenciamento.Domain.Models;
using Gerenciamento.Domain.Services;
using GerenciamentoApplication.Constants;
using GerenciamentoApplication.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace GerenciamentoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public ProjectController(IProjectService projetoService, IMapper mapper)
        {
            _projectService = projetoService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(UriTemplates.PROJECT)]
        public async Task<IActionResult> GetAllProjectsAsync()
        {
            var result = await _projectService.GetProjectsAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route(UriTemplates.PROJECT)]
        public async Task<IActionResult> CreateProjectAsync([FromBody] ProjectPost projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            await _projectService.SaveProjectAsync(project);
            return Ok();
        }

        [HttpDelete]
        [Route(UriTemplates.PROJECT)]
        public async Task<IActionResult> DeleTaskAsync([FromQuery] Guid id)
        {
            await _projectService.DeleteProjectAsync(id);
            return Ok();
        }

    }
}
