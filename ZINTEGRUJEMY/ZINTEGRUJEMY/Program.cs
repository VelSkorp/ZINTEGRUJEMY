using Microsoft.AspNetCore;

namespace ZINTEGRUJEMY
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebHost.CreateDefaultBuilder(args);

			builder.UseStartup<Startup>();

			var app = builder.Build();

			app.Run();
		}
	}
}