using CorrelationKernel;
using Handlers;
using Receiver;
using System;
using System.IO;
using System.Threading.Tasks;

namespace App
{
    class Program
    {

        static void Main(string[] args)
        {
            //Create dependecies
            var _elastic = new ElasticContext();
            var iishelper = new IISLogHelper();
            var receiver = new RabbitReceiver();
            var pipeline = new MessagePipeline(_elastic, iishelper);
            var correlationEngine = new CorrelationEngine(_elastic);
            //Init
            _elastic.Init();
            Console.WriteLine("init");
            Console.ReadKey();
           // pipeline.OnReceiveMessage_Pipeline("IISConnector:2021-05-10 18:13:04 192.168.0.103 GET /Home/Bad1avLogin - 80 - 192.168.0.116 200 0 0 3");
          //  pipeline.OnReceiveMessage_Pipeline("IISConnector:2021-05-11 18:13:04 192.168.0.103 GET /Home/Bad3avLogin - 80 - 192.168.0.116 200 0 0 3");
          // pipeline.OnReceiveMessage_Pipeline("IISConnector:2021-05-12 18:13:04 192.168.0.103 GET /Home/Bad4avLogin - 80 - 192.168.0.116 200 0 0 3");

            //Start proccesses
            Task.Run(() => correlationEngine.Start());
            //receiver.onMessageReceived += pipeline.OnReceiveMessage_Pipeline;
           // receiver.onMessageReceived += Receiver_onMessageReceived_InFile;
           // receiver.onMessageReceived += Receiver_onMessageReceived_InConsole;
            //receiver.Receive();
            Console.ReadKey();
        }
        private static void Receiver_onMessageReceived_InFile(string message)
        {
            using (StreamWriter sw = new StreamWriter(@"E:\database.txt", true, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(message);
            }
        }
        private static void Receiver_onMessageReceived_InConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
