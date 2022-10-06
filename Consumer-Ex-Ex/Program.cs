using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace Consumer_Ex_Ex
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };

			using var connection = factory.CreateConnection();

			using var channel = connection.CreateModel();

			channel.ExchangeDeclare(exchange: "secondexchange", type: ExchangeType.Fanout);

			channel.QueueDeclare("letterbox");

			channel.QueueBind("letterbox", "secondexchange", "");

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