using Serilog;
using System;
using DisplayLog.Extensions;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteToDisplayLogWeb("http://localhost:19222")
                .WriteTo.RollingFile($"D://Logs//{AppDomain.CurrentDomain.FriendlyName}//{DateTime.Now.ToString("yyMMdd")}.log")
                .CreateLogger();

            //测试日志数据
            Task.Run(() =>
            {
                for (int i = 0; ; i++)
                {
                    Log.Debug($"NO.{i} requestDemo.");
                    System.Threading.Thread.Sleep(3000);
                }
            });

            Console.ReadKey();
        }
    }
}
