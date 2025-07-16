using AutoMapper;

namespace ToDoListAPI.Application
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper<T1, T2>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}