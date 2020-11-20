using CustomerRegistration.API.Model;
using CustomerRegistration.API.Repository;
using CustomerRegistration.API.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomerRegistration.API
{
    /// <summary>
    /// This Startup class configures services and the app's request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        /// <summary>
        /// This method gets called by the runtime and used to  add services to the container. 
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Inject configuration using options pattern
            services.Configure<ValidatorConfig>(Configuration.GetSection("Validation"));

            // Configuring the DBContext. Set to use Sqlite database and also setting data source to connect.
            services.AddDbContext<CustomerContext>(opt =>
                                opt.UseSqlite("Data Source=Customer.db"));

            services.AddControllers();

            // Configure Fluent validation
            services.AddMvc().AddFluentValidation();

            // This enables ASP.NET to discover specific validator for customers.
            services.AddTransient<IValidator<Customer>, CustomerValidator>();

            // Injects Customer repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            // Adds Swagger
            services.AddSwaggerGen();
        }

        /// <summary>
        /// This method gets called by the runtime and used to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">See<see cref="IApplicationBuilder"/></param>
        /// <param name="env">See<see cref="IWebHostEnvironment"/></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Uses developer exception page in dev environment for better debugging
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // else RFC 7807-compliant payload to the client
                app.UseExceptionHandler("/error");
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
            });
        }
    }
}
