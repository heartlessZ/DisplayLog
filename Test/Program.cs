using Serilog;
using System;
using DisplayLog.Extensions;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteToDisplayLogWeb("http://localhost:57247")
                .CreateLogger();

            Log.Information("hh");

            Console.ReadKey();
        }
    }
}
