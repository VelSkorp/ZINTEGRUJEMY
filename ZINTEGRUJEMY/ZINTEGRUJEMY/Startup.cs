using Serilog;

namespace ZINTEGRUJEMY
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;

			Log.Logger = new LoggerConfiguration()
				.WriteTo.File(@"Logs\log.txt", rollingInterval: RollingInterval.Day)
				.WriteTo.Console()
				.CreateLogger();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			services.AddSingleton<CsvDownloader>();
			services.AddSingleton<CsvReader>();
			services.AddSingleton(provider =>
			{
				var configuration = provider.GetRequiredService<IConfiguration>();
				var connectionString = configuration.GetConnectionString("DefaultConnection");

				return new SqlWriter(connectionString);
			});
			services.AddSingleton(provider =>
			{
				var configuration = provider.GetRequiredService<IConfiguration>();
				var connectionString = configuration.GetConnectionString("DefaultConnection");

				return new SqlReader(connectionString);
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}