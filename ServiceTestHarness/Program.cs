using ServiceProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceTestHarness
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Service.");
            ServiceRunner.StartService();

            Console.WriteLine("Service started, press any key to stop the process.");
            Console.ReadKey();

            Console.WriteLine("Stopping Service.");
            ServiceRunner.StopService();

            while (!ServiceRunner.IsReadyToExit)
            {
                Console.WriteLine(".");
                Thread.Sleep(1000);
            }

            Console.Write(" Stopped." + Environment.NewLine);
            Console.WriteLine("Press any key to exit the test harness.");
            Console.ReadKey();
        }
    }
}
