using Serilog;
using System;
using DisplayLog.Extensions;
using System.Threading.Tasks;
using DisplayLog.Models;
using Microsoft.AspNet.SignalR.Client;
using DisplayLog.Web.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteToDisplayLogWeb("http://localhost:19222")
                .WriteTo.RollingFile("D://Logs//Test//{Date}.log")
                .CreateLogger();
            
            var connection = new HubConnectionBuilder().WithUrl("http://localhost:19222/pushLogHub")
                //.WithUrl("http://localhost:57247/pushLogHub", options =>
                //{
                //    options.HttpMessageHandlerFactory = handler =>
                //    {
                //        if (handler is HttpClientHandler httpClientHandler)
                //        {
                //            httpClientHandler.ServerCertificateCustomValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                //        }
                //        return handler;
                //    };
                //    options.WebSocketConfiguration = o =>
                //    {
                //        o.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                //    };
                //})
                .Build();

            connection.On("ConnectionSuccess", new Action<string>(PrintMessage));
            connection.On("ReciveMessage", new Action<string>(ReciveMessage));

            try
            {
                connection.StartAsync().Wait();
            }
            catch (Exception ex)
            {
                Log.Warning($"Connect SignalR Error :{ex.ToString()}");
            }

            //测试日志数据
            Task.Run(() =>
            {
                for (int i = 0; ; i++)
                {
                    Log.Debug($"NO.{i} requestdsfffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa.");
                    Log.Debug($"NO.{i} request.", new LogOptions()
                    {
                        Name = "Demo"
                    });
                    System.Threading.Thread.Sleep(3000);
                }
            });

            Console.ReadKey();
        }

        public static void PrintMessage(string log)
        {
            Log.Information($"Success{log}");
        }

        public static void ReciveMessage(object log)
        {
            Log.Information($"message{log.ToString()}");
        }
    }
}
