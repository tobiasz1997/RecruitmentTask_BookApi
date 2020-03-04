using AutoMapper;
using Books.Core.Domain;
using Books.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg => {
                cfg.CreateMap<Book, BookDTO>();
            }).CreateMapper();
    }
}
