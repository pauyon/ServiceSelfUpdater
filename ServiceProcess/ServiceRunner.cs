using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace ServiceProcess
{
    public class ServiceRunner
    {
        private static bool _serviceStarted = false;
        private static bool _readyToStop = true;

        public static bool IsReadyToExit { get { return !_serviceStarted && _readyToStop; } }

        public static void StartService()
        {
            _serviceStarted = true;
            ThreadPool.QueueUserWorkItem(StartServiceItemThread);
        }

        public static void StopService()
        {
            _serviceStarted = false;
        }

        public static void StartServiceItemThread(object state)
        {
            while (_serviceStarted)
            {
                _readyToStop = true;

                // Perform actions
                SimulateLongRunningTask();

                _readyToStop = true;
                Thread.Sleep(1000);
            }
        }

        private static void SimulateLongRunningTask()
        {
            //Thread.Sleep(1000);
            foreach (var i in Enumerable.Range(0, 9))
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "output.txt"), true))
                {
                    sw.WriteLine(string.Format("{0} - Process iterated - .exe v2", DateTime.Now.ToString().PadRight(30, ' ')));
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
