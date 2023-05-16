using CityBikeAPI.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace CityBikeAPI
{
    public class Startup
    {
        private readonly IConfiguration _config;

        private readonly string _connectionString;

        public Startup(IConfiguration config)
        {
            _config = config;
            _connectionString = $@"Server=db;port=3306; Database=citybike_fi; Uid=root; Pwd=1234";
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                services.AddCors(o => o.AddPolicy("AllowOrigins", builder =>
                {
                    builder.WithOrigins("*")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }));
            });
            WaitForDBInit(_connectionString);
            var connectionString = _connectionString;
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<CityBikeContext>(ops => ops.UseMySql(connectionString,serverVersion));
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CityBikeContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
            app.UseCors("AllowOrigins");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            context.Database.Migrate();
            app.UseMvc();
        }
        private static void WaitForDBInit(string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            int retries = 1;
            while (retries < 7)
            {
                try
                {
                    Console.WriteLine("Connecting to db. Trial: {0}", retries);
                    connection.Open();
                    connection.Close();
                    break;
                }
                catch (MySqlException)
                {
                    Thread.Sleep((int)Math.Pow(2, retries) * 1000);
                    retries++;
                }
            }
        }
    }
}
