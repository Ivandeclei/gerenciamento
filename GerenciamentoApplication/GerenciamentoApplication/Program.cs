using AutoMapper;
using Gerenciamento.Application;
using Gerenciamento.DbAdapter;
using Gerenciamento.DbAdapter.DbAdapterConfiguration;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Services;
using GerenciamentoApplication;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<Context>();
// Add DbContext with connection string from appsettings.json
var connectionString = builder.Configuration.GetSection("DbAdapterConfiguration:SqlConnectionString").Value;
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<Context>(options =>
              options.UseSqlServer(connectionString, c => c.MigrationsAssembly(typeof(Context).Assembly.FullName)));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "policyCors",
        policy =>
        {
            policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();

        });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IDbProjectReadAdapter), typeof(DbProjectReadAdapter));
builder.Services.AddScoped(typeof(IDbProjectWriteAdapter), typeof(DbProjectWriteAdapter));
builder.Services.AddScoped(typeof(IDbTaskReadAdapter), typeof(DbTaskReadAdapter));
builder.Services.AddScoped(typeof(IDbTaskWriteAdapter), typeof(DbTaskWriteAdapter));
builder.Services.AddScoped(typeof(IReportAdapter), typeof(ReportAdapter));
builder.Services.AddScoped(typeof(IUserDbAdapter), typeof(UserDbAdapter));
builder.Services.AddScoped(typeof(ILogUpdateAdapter), typeof(LogUpdateAdapter));


builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(WebApiMapperProfile),
                typeof(IMapper));

builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("policyCors");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
