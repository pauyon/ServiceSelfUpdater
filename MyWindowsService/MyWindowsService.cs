using ServiceProcess;
using System.ServiceProcess;

namespace MyWindowsService
{
    public partial class MyWindowsService : ServiceBase
    {
        public MyWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ServiceRunner.StartService();
        }

        protected override void OnStop()
        {
            ServiceRunner.StopService();
        }
    }
}
