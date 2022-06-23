using System.Text;
using RabbitMQ.Client;

Console.WriteLine("I'm publisher...");

var exchange = "topic_logs";
var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.ExchangeDeclare(exchange: exchange,
                            type: ExchangeType.Topic);

    var routingKey = (args.Length > 0) ? args[0] : "rabbitmq.info";

    var message = (args.Length > 1)
                  ? string.Join(" ", args.Skip(1).ToArray())
                  : "Hello World!";

    var body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: exchange,
                         routingKey: routingKey,
                         basicProperties: null,
                         body: body);
    Console.WriteLine("Sent msg '{0}' to '{1}'", message, routingKey);
}