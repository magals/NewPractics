using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;

namespace GrpcClient
{
	internal class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			using var channel = GrpcChannel.ForAddress("http://localhost:6054");
			var client = new MyProto.MyProtoClient(channel);

			ServiceStreamingCall(client);
			Console.ReadKey();
		}

		static Task ServiceStreamingCall(MyProto.MyProtoClient client)
		{
			var cts = new CancellationTokenSource();
			cts.CancelAfter(TimeSpan.FromSeconds(3.5));

		  using var call = client.SendMessageAsync(new MessageRequest
			{
				Name = 56
			}, cancellationToken: cts.Token);

			try
			{
				var asd = call.ResponseAsync.Result;
				Console.WriteLine("Server answer: " + asd.Answer);
				return Task.CompletedTask;
			}
			catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
			{
				Console.WriteLine("Stream cancelled.");
				return Task.CompletedTask;
			}
		}
	}
}