using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace backend.Core
{
    public interface IBaseApiControllerDependencies<T>
    {
        IMapper Mapper { get; }
        IConfiguration Configuration { get; }
        ILogger<T> Logger { get; }
    }

    public class BaseApiControllerDependencies<T> : IBaseApiControllerDependencies<T>
    {
        public BaseApiControllerDependencies(IMapper _mapper, IConfiguration _configuration, ILogger<T> _logger)
        {
            Mapper = _mapper;
            Configuration = _configuration;
            Logger = _logger;

        }
        public IMapper Mapper { get; private set; }
        public IConfiguration Configuration { get; private set;}
        public ILogger<T> Logger { get; private set; }
    }

    [Route("[controller]")]
    [ApiController]
    public class BaseApiController<T> : ControllerBase
    {
        public readonly IMapper Mapper;
        public readonly IConfiguration Configuration;
        public readonly ILogger<T> Logger;

        public BaseApiController(IBaseApiControllerDependencies<T> dependencies)
        {
            Configuration = dependencies.Configuration;
            Mapper = dependencies.Mapper;
            Logger = dependencies.Logger;
        }

        protected string GetConfigurationValue1(string configurationName)
        {
            return Configuration[configurationName].ToString();
        }
    }
}
