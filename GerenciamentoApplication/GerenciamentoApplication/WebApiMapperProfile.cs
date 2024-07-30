using AutoMapper;
using Gerenciamento.Domain.Models;
using GerenciamentoApplication.Dtos;

namespace GerenciamentoApplication
{
    public class WebApiMapperProfile : Profile
    {
        public WebApiMapperProfile()
        {
            // Mapping for User and UserDto
            CreateMap<User, UserDto>()
                .ReverseMap();

            // Mapping for TaskProject and TaskDto
            CreateMap<TaskProject, TaskDto>()
                .ReverseMap();

            // Mapping for TaskPost and TaskProject
            CreateMap<TaskPost, TaskProject>()
                .ForPath(dest => dest.Title, opt => opt.MapFrom(src => src.Task.Title))
                .ForPath(dest => dest.Description, opt => opt.MapFrom(src => src.Task.Description))
                .ForPath(dest => dest.DueDate, opt => opt.MapFrom(src => src.Task.DueDate))
                .ForPath(dest => dest.Status, opt => opt.MapFrom(src => src.Task.Status))
                .ForPath(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForPath(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Task.ProjectId))
                .ReverseMap();

            // Mapping for ReportTask and ReportResultDto
            CreateMap<ReportTask, ReportResultDto>()
                .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForPath(dest => dest.NumberOfTask, opt => opt.MapFrom(src => src.NumberOfTask))
                .ReverseMap();

            // Mapping for User and UserPost
            CreateMap<User, UserPost>()
                .ReverseMap();

            // Mapping for TaskUpdate and TaskProject
            CreateMap<TaskUpdate, TaskProject>()
                .ForPath(dest => dest.Title, opt => opt.MapFrom(src => src.Task.Title))
                .ForPath(dest => dest.Description, opt => opt.MapFrom(src => src.Task.Description))
                .ForPath(dest => dest.DueDate, opt => opt.MapFrom(src => src.Task.DueDate))
                .ForPath(dest => dest.Status, opt => opt.MapFrom(src => src.Task.Status))
                .ForPath(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Task.ProjectId))
                .ReverseMap();

            // Mapping for TaskPriorityDto and TaskProject
            CreateMap<TaskPriorityDto, TaskProject>()
                .ForPath(dest => dest.Title, opt => opt.MapFrom(src => src.Task.Title))
                .ForPath(dest => dest.Description, opt => opt.MapFrom(src => src.Task.Description))
                .ForPath(dest => dest.DueDate, opt => opt.MapFrom(src => src.Task.DueDate))
                .ForPath(dest => dest.Status, opt => opt.MapFrom(src => src.Task.Status))
                .ForPath(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForPath(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Task.ProjectId))
                .ReverseMap();

            // Mapping for Project and ProjectDto
            CreateMap<Project, ProjectDto>()
                .ReverseMap();

            // Mapping for ProjectPost and Project
            CreateMap<ProjectPost, Project>()
                .ReverseMap();

            // Mapping for Comments and CommentsDto
            CreateMap<Comments, CommentsDto>()
                .ReverseMap();

            // Mapping for DeleteBaseDto and BasePost
            CreateMap<DeleteBaseDto, BasePost>()
                .ReverseMap();

            // Mapping lists
            CreateMap<List<ReportTask>, List<ReportResultDto>>()
                .ConvertUsing(src => src.Select(reportTask => new ReportResultDto
                {
                    Name = reportTask.User.Name,
                    NumberOfTask = reportTask.NumberOfTask
                }).ToList());
        }
    }
}
