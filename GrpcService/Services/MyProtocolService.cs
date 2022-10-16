using Grpc.Core;
using GrpcService;

namespace GrpcService.Services
{
	public class MyProtocolService : MyProto.MyProtoBase
	{
		private readonly ILogger<MyProtocolService> _logger;
		public MyProtocolService(ILogger<MyProtocolService> logger)
		{
			_logger = logger;
		}

		public override Task<MessageReply> SendMessage(MessageRequest request, ServerCallContext context)
		{
			_logger.LogInformation("Server answer");
			return Task.FromResult(new MessageReply
			{
				Answer = 42
			});
		}
	}
}
