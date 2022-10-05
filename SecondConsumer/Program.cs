using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory { HostName = "localhost" };
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			channel.ExchangeDeclare(exchange: "pubsub", type: ExchangeType.Fanout);

			var queuename = channel.QueueDeclare().QueueName;

			
			var consumer = new EventingBasicConsumer(channel);

			channel.QueueBind(queuename, "pubsub", "");

			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				Console.WriteLine($"Seconf consumer - Recieved new message: {message}");

			};

			channel.BasicConsume(queue: queuename, autoAck: true, consumer: consumer);
			Console.WriteLine($"Consuming");
			Console.ReadKey();

		}
	}
}