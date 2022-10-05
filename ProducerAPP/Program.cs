using RabbitMQ.Client;
using System.Text;

namespace ProducerAPP
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory { HostName = "localhost" };
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			channel.ExchangeDeclare("mytopicexchange", ExchangeType.Topic);

			var userPaymentMessage = $"A european user paid for something";
			var userPaymentbody = Encoding.UTF8.GetBytes(userPaymentMessage);
			channel.BasicPublish(
				"mytopicexchange",
				"user.europe.payments", //"paymentssonly",//analyticsonly
				null,
				userPaymentbody);
			Console.WriteLine($"Send message:{userPaymentbody}");

			var businessPaymentMessage = $"A european user paid for something";
			var businessPaymentbody = Encoding.UTF8.GetBytes(businessPaymentMessage);
			channel.BasicPublish(
				"mytopicexchange",
				"business.europe.order", //"paymentssonly",//analyticsonly
				null,
				userPaymentbody);
			Console.WriteLine($"Send message:{businessPaymentbody}");
		}
	}
}