using Books.Api.Extensions;
using Books.Common.Options;
using Books.Core.Repositories;
using Books.Infrastructure.Mappers;
using Books.Infrastructure.Repositories;
using Books.Infrastructure.Services;
using Books.MySqlDatabase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Books.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                //.AddDbContext<BooksContext>()
                //.Configure<DatabaseOptions>(Configuration.GetSection(nameof(DatabaseOptions)))
                .AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
                }).AddControllers();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddSingleton(AutoMapperConfig.Initialize());;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        //    var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        //    using var scope = scopeFactory.CreateScope();
        //    var context = scope.ServiceProvider.GetService<BooksContext>();

        //    context.Database.EnsureCreated();
        }
    }
}
