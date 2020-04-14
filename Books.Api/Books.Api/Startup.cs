using Books.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Books.Api
{
    using Database.Repositories;
    using Common.Options;
    using Core.Repositories;
    using Infrastructure.Mappers;
    using Infrastructure.Services;
    using MySqlDatabase;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
                x.ApiVersionReader = new HeaderApiVersionReader("book-version");
                //x.ApiVersionReader = ApiVersionReader.Combine(
                //    new HeaderApiVersionReader("NCV-Version"),
                //    new QueryStringApiVersionReader("v"));
            });
            services.AddVersionedApiExplorer(x => x.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(x =>
            {
                x.OperationFilter<SwaggerDefaultValues>();
            });
            services
                .AddDbContext<BooksContext>()
                .Configure<DatabaseOptions>(Configuration.GetSection(nameof(DatabaseOptions)))
                .AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
                }).AddControllers();


            //services.AddSwaggerGen(x =>
            //{
            //    x.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "BookApi",
            //        Version = "V1"
            //    });
            //    x.OperationFilter<SwaggerDefaultValues>();
            //});

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddSingleton(AutoMapperConfig.Initialize());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                //x.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //x.RoutePrefix = string.Empty;
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    x.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                    //x.RoutePrefix = $"app{description.ApiVersion.ToString()}";
                }
            });

            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<BooksContext>();

            context.Database.EnsureCreated();
        }
    }
}