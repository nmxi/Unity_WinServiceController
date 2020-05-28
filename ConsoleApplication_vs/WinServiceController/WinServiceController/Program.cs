using System;
using System.ServiceProcess;

namespace WinServiceController
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
                return;

            switch (args[0])
            {
                case "GET":
                    //Console.WriteLine("get service list");

                    GetServices(args[1]);

                    break;
                case "START":
                    //Console.WriteLine("start service");

                    StartService(args[1]);

                    break;
                case "STOP":
                    //Console.WriteLine("stop service");

                    StopService(args[1]);

                    break;
                case "RESTART":
                    //Console.WriteLine("restart service");

                    RestartService(args[1]);

                    break;
                default:
                    //Console.WriteLine(Environment.CommandLine + " is unknown method");
                    break;
            }
        }

        /// <summary>
        /// サービス一覧を取得する
        /// </summary>
        /// <param name="serviceName"></param>
        private static void GetServices(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                return;

            ServiceController[] scSercices;
            scSercices = ServiceController.GetServices();

            var flag = false;
            foreach (var scTemp in scSercices)
            {
                if (scTemp.ServiceName == serviceName)
                {
                    Console.WriteLine(scTemp.ServiceName + " STATUS:" + scTemp.Status);
                    flag = true;
                }
            }

            if (!flag)
                Console.WriteLine("not found service : " + serviceName);
        }

        /// <summary>
        /// サービスを起動する
        /// </summary>
        /// <param name="serviceName"></param>
        private static void StartService(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                return;

            try
            {
                ServiceController sc = new ServiceController();
                sc.ServiceName = serviceName;

                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);
                    sc.Refresh();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Could not start the service");
            }

            Console.WriteLine("started " + serviceName);
        }

        /// <summary>
        /// サービスを停止する
        /// </summary>
        /// <param name="serviceName"></param>
        private static void StopService(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                return;

            try
            {
                ServiceController sc = new ServiceController();
                sc.ServiceName = serviceName;

                if (sc.Status == ServiceControllerStatus.Running)
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);
                    sc.Refresh();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Could not stop the service");
            }

            Console.WriteLine("Stoped " + serviceName);
        }

        /// <summary>
        /// サービスを再起動する
        /// </summary>
        /// <param name="serviceName"></param>
        private static void RestartService(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                return;

            StopService(serviceName);
            StartService(serviceName);
        }
    }
}
