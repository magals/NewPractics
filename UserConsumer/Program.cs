using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace UserConsumer
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
			channel.QueueBind(queueName, "mytopicexchange", "user.#");
			var consumer = new EventingBasicConsumer(channel);

			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				Console.WriteLine($"User - Recieved new message:{message}");
			};

			channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

			Console.WriteLine($"User - Consumer");

			Console.ReadKey();

		}
	}
}