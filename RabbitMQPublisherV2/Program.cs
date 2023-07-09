using System.Text;
using RabbitMQ.Client;

#region Base
//Connection
ConnectionFactory factory = new();
factory.Uri = new("amqps://omqimsrq:4loIcS64HHTLB04sDnzMtji9C_5GYgIa@woodpecker.rmq.cloudamqp.com/omqimsrq");

//Connection activation and channel opening
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();
#endregion

#region Fanout Exchange

channel.ExchangeDeclare("logs-fanout", durable: true, type: ExchangeType.Fanout);

Enumerable.Range(1, 50).ToList().ForEach(x =>
{
    string message = $"log {x}";

    var messageBody = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish("logs-fanout", "", null, messageBody);

    Console.WriteLine($"Mesaj Gönderilmiştir : {message}");
});

#endregion

//#region Work Queue

//channel.QueueDeclare("hello-queue", true, false, false);

//Enumerable.Range(1, 50).ToList().ForEach(x =>
//{
//    string message = $"Message {x}";

//    var messageBody = Encoding.UTF8.GetBytes(message);

//    channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

//    Console.WriteLine($"Mesaj Gönderilmiştir : {message}");
//});

//#endregion

Console.ReadLine();