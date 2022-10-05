using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace Client_RR
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory { HostName = "localhost" };
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			channel.QueueDeclare(queue: "",
				exclusive: true);

			var replyQueue = channel.QueueDeclare("request-queue", exclusive: false);

			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				Console.WriteLine($"Reply Recieved: {message}");
			};

			channel.BasicConsume(queue: replyQueue.QueueName, autoAck: false, consumer: consumer);

			var message = "Can i request a reply";
			var body = Encoding.UTF8.GetBytes(message);

			var properties = channel.CreateBasicProperties();
			properties.ReplyTo = replyQueue.QueueName;
			properties.CorrelationId = Guid.NewGuid().ToString();
			channel.BasicPublish("", "request-queue", properties, body);
			Console.WriteLine($"Sending Request:{properties.CorrelationId}");
			Console.WriteLine($"Stared client");
			Console.ReadKey();

		}
	}
}