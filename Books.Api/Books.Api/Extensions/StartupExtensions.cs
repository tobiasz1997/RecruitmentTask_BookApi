namespace Books.Api.Extensions
{
    using Microsoft.Extensions.Configuration;

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