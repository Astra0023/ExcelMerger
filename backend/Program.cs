using backend.AutoMapperProfiles;
using backend.Context;
using backend.Core;
using backend.Infrastructure.Swagger;
using backend.Interface;
using backend.Repository;
using backend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;


var _appName = typeof(Program).Namespace;
var builder = WebApplication.CreateBuilder(args);
var _configuration = builder.Configuration;


// Add services to the container.
builder.Services.AddDbContext <ExcelMergerDbContext>();
builder.Services.AddTransient(typeof(IBaseApiControllerDependencies<>), typeof(BaseApiControllerDependencies<>));
builder.Services.AddTransient(typeof(IBaseDependenciesService<>), typeof(BaseDependenciesService<>));
builder.Services.AddTransient<IExcelMergerService, ExcelMergerService>();
builder.Services.AddTransient<IExcelMergerRepository, ExcelMergerRepository>();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureSwagger(_configuration, "Excel Merger Api", "1");
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oath2", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as the following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("LocalDevelopment"))
{
    app.UseDeveloperExceptionPage();
}else
{
    app.UseHsts();
}

var forwardedHeadersOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
};
forwardedHeadersOptions.KnownNetworks.Clear();
forwardedHeadersOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardedHeadersOptions);
app.UseSwaggerFeatures(_configuration, app.Environment, "", "Excel Merger Api");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
