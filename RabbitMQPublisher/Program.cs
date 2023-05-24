using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

//Connection
ConnectionFactory factory = new();
factory.Uri = new("amqps://omqimsrq:4loIcS64HHTLB04sDnzMtji9C_5GYgIa@woodpecker.rmq.cloudamqp.com/omqimsrq");

//Connection activation and channel opening
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue creating
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Queue Message Send
byte[] message = Encoding.UTF8.GetBytes("Hello");

//Default Exchange Send
channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

Console.Read();

#region Message Durability
////Connection
//ConnectionFactory factory = new();
//factory.Uri = new("");

////Connection activation and channel opening
//using IConnection connection = factory.CreateConnection();
//using IModel channel = connection.CreateModel();

//IBasicProperties basicProperties = channel.CreateBasicProperties();
//basicProperties.Persistent = true;
////Queue creating
//channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);

////Queue Message Send
//byte[] message = Encoding.UTF8.GetBytes("Hello");

////Default Exchange Send
//channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message, basicProperties: basicProperties);

//Console.Read();
#endregion