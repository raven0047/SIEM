using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Receiver
{
    public class RabbitReceiver
    {
        string QueueName { set; get; }

        public delegate void onReceiveMessage(string message);
        public event onReceiveMessage onMessageReceived;

        public RabbitReceiver()
        {
            QueueName = "TestQueue";
        }
        public async Task Receive()
        {

            ConnectionFactory connectionFactory = new ConnectionFactory() { HostName = "localhost" };

            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, false, false, false, null);
                    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (o, e) =>
                    {
                        var body = e.Body.ToArray();
                        string str = Encoding.UTF8.GetString(body);

                        onMessageReceived(str);

                    };

                    string consumerTag = channel.BasicConsume(QueueName, true, consumer);
                    await Task.Delay(-1);
                }
            }
        }
    }
}
