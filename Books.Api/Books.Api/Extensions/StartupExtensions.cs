using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Api.Extensions
{
    public static class StartupExtensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration config, 
            string section) where TModel : new()
        {
            var model = new TModel();
            config.GetSection(section).Bind(model);

            return model;
        }
    }
}
