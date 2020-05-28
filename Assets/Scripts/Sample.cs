using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace nmxi.winservicecontroller
{
    [RequireComponent(typeof(WinServiceController))]
    public class Sample : MonoBehaviour
    {
        WinServiceController wsc;

        private void Awake()
        {
            wsc = GetComponent<WinServiceController>();
        }

        void Start()
        {
            wsc.GetStatus();
        }

        public void UpdateServiceName(InputField i)
        {
            wsc.SetServiceName(i.text);
        }
    }
}