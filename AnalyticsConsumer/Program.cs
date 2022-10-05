using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace AnalyticsConsumer
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory { HostName = "localhost" };
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			channel.ExchangeDeclare("mytopicexchange", ExchangeType.Topic);

			var queueName = channel.QueueDeclare().QueueName;
			channel.QueueBind(queueName, "mytopicexchange", "*.europe.*");
			var consumer = new EventingBasicConsumer(channel);

			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				Console.WriteLine($"Analytics - Recieved new message:{message}");
			};

			channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

			Console.WriteLine($"Analytics - Consumer");

			Console.ReadKey();

		}
	}
}