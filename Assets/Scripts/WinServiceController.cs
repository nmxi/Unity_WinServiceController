using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace nmxi.winservicecontroller
{
    public class WinServiceController : MonoBehaviour
    {
        [SerializeField] private string ControlServiceName;

        [Space(15f), SerializeField] private Text statusShowText;

        private readonly string winServiceControllerPath = Application.streamingAssetsPath + "/WinServiceController/WinServiceController.exe";

        /// <summary>
        /// Setting the service name of the control target.
        /// </summary>
        /// <param name="newServiceName"></param>
        public void SetServiceName(string newServiceName)
        {
            ControlServiceName = newServiceName;
        }

        /// <summary>
        /// Returns the operating status of the current service.
        /// </summary>
        public void GetStatus()
        {
            statusShowText.text = WSCGet();
        }

        /// <summary>
        /// Start service.
        /// </summary>
        public void StartService()
        {
            WSCStart();
        }

        /// <summary>
        /// Stop service.
        /// </summary>
        public void StopService()
        {
            WSCStop();
        }

        #region WSCController

        private string WSCGet()
        {
            var p = new Process();
            p.StartInfo.FileName = winServiceControllerPath;
            p.StartInfo.Arguments = "GET \"" + ControlServiceName + "\"";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();

            var stdoutStr = p.StandardOutput.ReadToEnd();

            p.WaitForExit();

            stdoutStr = stdoutStr.Replace("\r\r\n", "\n");

            p.Close();

            return stdoutStr;
        }

        private void WSCStart()
        {
            var p = new Process();
            p.StartInfo.FileName = winServiceControllerPath;
            p.StartInfo.Verb = "RunAs";
            p.StartInfo.Arguments = "START \"" + ControlServiceName + "\"";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = true;
            p.Start();
            p.WaitForExit();
            p.Close();
        }

        private void WSCStop()
        {
            var p = new Process();
            p.StartInfo.FileName = winServiceControllerPath;
            p.StartInfo.Verb = "RunAs";
            p.StartInfo.Arguments = "STOP \"" + ControlServiceName + "\"";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = true;
            p.Start();
            p.WaitForExit();
            p.Close();
        }

        #endregion
    }
}