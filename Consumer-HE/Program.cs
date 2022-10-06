using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer_HE
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };

			using var connection = factory.CreateConnection();

			using var channel = connection.CreateModel();

			channel.ExchangeDeclare(exchange: "headerexchange", type: ExchangeType.Headers);

			channel.QueueDeclare("letterbox");

			var bindingArgument = new Dictionary<string, object>
			{
				{"x-match", "all" },
				{"name", "brian" },
				{"age", "ew" },
			};

			channel.QueueBind("letterbox", "headerexchange", "", bindingArgument);

			var consumer = new EventingBasicConsumer(channel);

			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				Console.WriteLine($"Recieved new message: {message}");
			};

			channel.BasicConsume(queue: "letterbox", autoAck: true, consumer: consumer);

			Console.WriteLine("Consuming");

			Console.ReadKey();

		}
	}
}