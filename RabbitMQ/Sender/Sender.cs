//https://github.com/rabbitmq/rabbitmq-tutorials/blob/main/dotnet/Send/Send.cs

using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "queue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

while (true)
{
    Console.Clear();

    Console.WriteLine($"Write a message to send");
    string? message = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(message))
    {
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty,
                             routingKey: "queue",
                             basicProperties: null,
                             body: body);
    }
}