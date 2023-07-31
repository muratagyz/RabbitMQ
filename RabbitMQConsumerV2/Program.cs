using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

channel.BasicQos(0, 1, false);
var consumer = new EventingBasicConsumer(channel);

var queueName = channel.QueueDeclare().QueueName;
var routekey = "*.Error.*";
channel.QueueBind(queueName, "logs-topic", routekey);

channel.BasicConsume(queueName, false, consumer);

consumer.Received += (sender, eventArgs) =>
{
    var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

    Thread.Sleep(1500);

    Console.WriteLine($"Gelen Mesaj : {message}");

    channel.BasicAck(eventArgs.DeliveryTag, false);
};


#endregion

//#region Fanout Exchange

//var randomQueueName = channel.QueueDeclare().QueueName;

//channel.QueueBind(randomQueueName, "logs-fanout", "", null);

//channel.BasicQos(0, 1, false);

//var consumer = new EventingBasicConsumer(channel);

//channel.BasicConsume(randomQueueName, false, consumer);

//consumer.Received += (sender, eventArgs) =>
//{
//    var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

//    Thread.Sleep(1500);

//    Console.WriteLine($"Gelen Mesaj : {message}");

//    channel.BasicAck(eventArgs.DeliveryTag, false);
//};

//#endregion

//#region Work Queue

//channel.BasicQos(0, 1, false);

//var consumer = new EventingBasicConsumer(channel);

//channel.BasicConsume("hello-queue", false, consumer);

//consumer.Received += (sender, eventArgs) =>
//{
//    var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

//    Thread.Sleep(1500);

//    Console.WriteLine($"Gelen Mesaj : {message}");

//    channel.BasicAck(eventArgs.DeliveryTag, false);
//};

//#endregion

Console.ReadLine();