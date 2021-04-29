using System;
using System.IO;
using Receiver;

namespace App
{
    public class Kernel
    {
        public void Start()
        {
            RabbitReceiver receiver = new RabbitReceiver();
            receiver.onMessageReceived += Receiver_onMessageReceived;
            receiver.Receive();
        }

        private void Receiver_onMessageReceived(string message)
        {
            using (StreamWriter sw = new StreamWriter(@"E:\database.txt", true, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(message);
            }
        }

    }
}
