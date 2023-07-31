using System.Text;
using RabbitMQ.Client;
using RabbitMQPublisherV2;

#region Base
//Connection
ConnectionFactory factory = new();
factory.Uri = new("amqps://omqimsrq:4loIcS64HHTLB04sDnzMtji9C_5GYgIa@woodpecker.rmq.cloudamqp.com/omqimsrq");

//Connection activation and channel opening
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();
#endregion


#region Topic Exchange
channel.ExchangeDeclare("logs-topic", durable: true, type: ExchangeType.Topic);

Random rnd = new Random();
Enumerable.Range(1, 50).ToList().ForEach(x =>
{
    LogNames log1 = (LogNames)rnd.Next(1, 5);
    LogNames log2 = (LogNames)rnd.Next(1, 5);
    LogNames log3 = (LogNames)rnd.Next(1, 5);

    string routeKey = $"{log1}.{log2}.{log3}";
    string message = $"log-type: {log1}-{log2}-{log3}";
    var messageBody = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish("logs-topic", routeKey, null, messageBody);

    Console.WriteLine($"Mesaj Gönderilmiştir : {message}");
});


#endregion

//#region Fanout Exchange

//channel.ExchangeDeclare("logs-fanout", durable: true, type: ExchangeType.Fanout);

//Enumerable.Range(1, 50).ToList().ForEach(x =>
//{
//    string message = $"log {x}";

//    var messageBody = Encoding.UTF8.GetBytes(message);

//    channel.BasicPublish("logs-fanout", "", null, messageBody);

//    Console.WriteLine($"Mesaj Gönderilmiştir : {message}");
//});

//#endregion

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