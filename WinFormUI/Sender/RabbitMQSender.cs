using System;
using RabbitMQ.Client;
using System.Text;
using System.Collections.Generic;
using Settings;

namespace Sender
{
    public class RabbitMQSender
    {
        string Uri { set; get; }
        string QueueName { set; get; }
        string ConnectorName { set; get; }
        public RabbitMQSender(string connectorName)
        {          
            ConnectorName = connectorName;

            SettingsHandler settings = new SettingsHandler();
            Uri = settings.Rabbit.IpServer;
            QueueName = settings.Rabbit.QueueName;
        }

        public void Send(List<string> messageList)
        {
            if (messageList.Count > 0)
            {
                ConnectionFactory factory = new ConnectionFactory() { HostName = Uri };
                using (var rConnection = factory.CreateConnection())
                using (var channel = rConnection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueName, durable: false,
                        exclusive: false, autoDelete: false, arguments: null);

                    foreach (var message in messageList)
                    {
                        byte[] body = Encoding.UTF8.GetBytes(ConnectorName + ":" + message);
                        channel.BasicPublish(exchange: "", routingKey: QueueName,
                            basicProperties: null, body: body);
                    }
                }

            }
        }
    }

}

