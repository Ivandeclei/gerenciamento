using AutoMapper;
using Gerenciamento.Domain.Models;
using Gerenciamento.Domain.Services;
using GerenciamentoApplication.Constants;
using GerenciamentoApplication.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;
        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(UriTemplates.TASK_FIND_BY_ID_PROJECT)]
        public async Task<IActionResult> GetAllTasksByProjectAsync([FromQuery] Guid idProject)
        {
            var result = await _taskService.GetTaskAsync(idProject);
            return Ok(result);
        }

        [HttpPost]
        [Route(UriTemplates.TASK)]
        public async Task<IActionResult> CreateTaskAsync([FromBody] TaskPost taskDto)
        {
            var task = _mapper.Map<TaskProject>(taskDto);
            var user = _mapper.Map<User>(taskDto);
            await _taskService.SaveTaskAsync(task, user);


            return Ok();
        }

        [HttpPost]
        [Route(UriTemplates.TASK_COMMENT)]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CommentsDto commentDto)
        {
            var comment = _mapper.Map<Comments>(commentDto);
            var user = _mapper.Map<User>(commentDto);
            await _taskService.SaveCommentAsync(comment, user);


            return Ok();
        }

        [HttpPut]
        [Route(UriTemplates.TASK)]
        public async Task<IActionResult> UpdateTaskAsync([FromBody] TaskUpdate taskDto)
        {
            var task = _mapper.Map<TaskProject>(taskDto);
            var user = _mapper.Map<User>(taskDto);
            await _taskService.UpdateTaskAsync(task, user);
            return Ok();
        }

        [HttpDelete]
        [Route(UriTemplates.TASK)]
        public async Task<IActionResult> DeleTaskAsync([FromBody] DeleteBaseDto deleteBaseDto)
        {
            var user = _mapper.Map<User>(deleteBaseDto);
            await _taskService.DeleteTaskAsync(deleteBaseDto.Id, user);
            return Ok();
        }
    }
}
