namespace Books.Infrastructure.Mappers
{
    using AutoMapper;
    using Core.Domain;
    using DTO;

    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookDto>();
            }).CreateMapper();
    }
}
