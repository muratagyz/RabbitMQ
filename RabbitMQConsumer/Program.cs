using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

#region Default : Round-Robin Dispatching
//Connection
ConnectionFactory factory = new();
factory.Uri = new("amqps://omqimsrq:4loIcS64HHTLB04sDnzMtji9C_5GYgIa@woodpecker.rmq.cloudamqp.com/omqimsrq");

//Connection activation and channel opening
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue creating
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Queue message reading
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);
consumer.Received += (sender, e) =>
{
    //Queue get messages
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();
#endregion

#region Message Acknowledgement
////Connection
//ConnectionFactory factory = new();
//factory.Uri = new("amqps://omqimsrq:4loIcS64HHTLB04sDnzMtji9C_5GYgIa@woodpecker.rmq.cloudamqp.com/omqimsrq");

////Connection activation and channel opening
//using IConnection connection = factory.CreateConnection();
//using IModel channel = connection.CreateModel();

////Queue creating
//channel.QueueDeclare(queue: "example-queue", exclusive: false);

////Queue message reading
//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);
//consumer.Received += (sender, e) =>
//{
//    //Queue get messages
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//    Task.Delay(3000);

//    channel.BasicAck(e.DeliveryTag, multiple: false);
//};

//Console.Read();
#endregion
