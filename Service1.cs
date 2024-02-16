using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace WindowsServiceDemo
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        EventLog eventlog = new EventLog();
        UpdateRecord obj = new UpdateRecord();
        public Service1()
        {
            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service started at " + DateTime.Now);
            timer.Interval = TimeSpan.FromMinutes(10).TotalMilliseconds;
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

            obj.fetchAndUpdateData();
        }

        protected override void OnStop()
        {
            //WriteToFile("Service stopped at " + DateTime.Now);
            timer.Stop();
            timer.Dispose();

        }



        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            obj.fetchAndUpdateData();
        }

        public void WriteToFile(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                }
            }

        }
    }
}
