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

#region  Direct Exchange

channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

while (true)
{
    Console.Write("Mesaj : ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: "direct-exchange-example", routingKey: "direct-queue-example", body: byteMessage);
}

#endregion

#region Default
//Queue creating
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Queue Message Send
byte[] message2 = Encoding.UTF8.GetBytes("Hello");

//Default Exchange Send
channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message2);

Console.Read();
#endregion


#region Message Durability
//Connection
ConnectionFactory factory2 = new();
factory2.Uri = new("");

//Connection activation and channel opening
using IConnection connection2 = factory2.CreateConnection();
using IModel channel2 = connection2.CreateModel();

IBasicProperties basicProperties = channel.CreateBasicProperties();
basicProperties.Persistent = true;
//Queue creating
channel2.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);

//Queue Message Send
byte[] message3 = Encoding.UTF8.GetBytes("Hello");

//Default Exchange Send
channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message3, basicProperties: basicProperties);

Console.Read();
#endregion