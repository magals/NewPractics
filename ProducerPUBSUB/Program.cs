using RabbitMQ.Client;
using System.Text;

namespace Producer
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory { HostName = "localhost" };
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			channel.ExchangeDeclare(exchange: "pubsub", type: ExchangeType.Fanout);

			var message = $"Hello i want to broadcast this message";
			var encoderMessage = Encoding.UTF8.GetBytes(message);
			channel.BasicPublish("pubsub", "letterbox", null, encoderMessage);
			Console.WriteLine($"Send message:{message}");

		}
	}
}