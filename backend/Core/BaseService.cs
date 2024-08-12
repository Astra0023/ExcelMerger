using AutoMapper;

namespace backend.Core
{

    public interface IBaseDependenciesService<T>
    {
        IMapper Mapper { get; }
        IConfiguration Configuration { get; }
    }

    public class BaseDependenciesService<T> : IBaseDependenciesService<T>
    {
        public BaseDependenciesService(IMapper mapper, IConfiguration configuration)
        {
            Mapper = mapper;
            Configuration = configuration;

        }
        public IMapper Mapper { get; private set; }
        public IConfiguration Configuration { get; private set; }
    }

    public class BaseService<T>
    {
        public BaseService(IBaseDependenciesService<T> dependencies)
        {
            Mapper = dependencies.Mapper;
            Configuration = dependencies.Configuration;
        }
        public IMapper Mapper { get; set; }
        public IConfiguration Configuration { get; set;}
    }
}
