using Microsoft.OpenApi.Models;
using System.Reflection;


namespace backend.Infrastructure.Swagger
{
    public static class SwaggerStatup
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration Configuration, string swaggerTitle = "Excel Merger Api", string apiVersion = "1")
        {
            services.AddEndpointsApiExplorer();
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(String.Format("v1", apiVersion), new OpenApiInfo { Title = swaggerTitle, Version = String.Format("v{0}", apiVersion) });
                c.UseInlineDefinitionsForEnums();
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerFeatures(this IApplicationBuilder app, IConfiguration config, IWebHostEnvironment env, string swaggerBasePath, string swaggerTitle = "Excel Merger Api")
        {
            app.UseSwaggerUI(c =>
            {
                string swaggerPath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerPath}/swagger/v1/swagger.json", swaggerTitle);
            });

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    if (httpReq.Headers.ContainsKey("X-Forwarded-Host"))
                    {
                        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = swaggerBasePath } };
                    }
                });
            });

            return app;
        }
    }
}
