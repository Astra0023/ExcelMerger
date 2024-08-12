using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace backend.Infrastructure.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IConfiguration _config;
        public ConfigureSwaggerOptions(IConfiguration config)
        {
            _config = config;
        }

        public void Configure(SwaggerGenOptions options)
        {
            var disco = GetDiscoveryDocument();
            var apiScore = _config.GetValue<string>("Auth0:Audience");
            var scopes = apiScore.Split(new[] { ' '}, StringSplitOptions.RemoveEmptyEntries).ToList();

            var addScopes = _config.GetValue<string>("Auth0:AdditionalScopes");
            var additonalScopes = addScopes.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            scopes.AddRange(additonalScopes);

            var oauthScopeDisc = new Dictionary<string, string>();
            foreach(var scope in scopes)
            {
                oauthScopeDisc.Add(scope, $"Resource access: {scope}"); 
            }

            options.EnableAnnotations();
            options.AddSecurityDefinition("oath2", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the Bearer Authorization string as the following: `Bearer Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        }

        private DiscoveryDocumentResponse GetDiscoveryDocument()
        {
            var client = new HttpClient();
            var authority = _config.GetValue<string>("Auth0:Domain");
            return client.GetDiscoveryDocumentAsync(authority)
                .GetAwaiter().
                GetResult();
        }
    }
}
