using System.Text;
using RabbitMQ.Client;

Console.WriteLine("I'm publisher...");

var queueName = "post-office";
var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(
        queue: queueName,
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);

    string message = "Hello World!";
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: "",
        routingKey: queueName,
        basicProperties: null,
        body: body);

    Console.WriteLine("Sent: {0}", message);
}

Console.ReadLine();