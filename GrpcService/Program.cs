using GrpcService.Services;

namespace GrpcService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddGrpc();

			var app = builder.Build();

			app.MapGrpcService<MyProtocolService>();

			app.Run("http://localhost:6054");
		}
	}
}