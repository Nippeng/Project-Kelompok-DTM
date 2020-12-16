/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script editor untuk memudahkan pembuatan quit dialog
 **************************************************************************************************************/

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class QuitController : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;

        [Header("Invoke Settings")]
        public GlobalVariable.CInvokeType InvokeType;

        [Header("Delay Settings")]
        public bool usingDelay;
        public float Delay;

        [Header("Interval Settings")]
        public bool usingInterval;
        public float Interval;
        
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
                {
                    InvokeQuitController();
                }
                if (InvokeType == GlobalVariable.CInvokeType.OnDelay)
                {
                    if (usingDelay)
                    {
                        Invoke("InvokeQuitController", Delay);
                    }
                }
                if (InvokeType == GlobalVariable.CInvokeType.OnInterval)
                {
                    if (usingInterval)
                    {
                        InvokeRepeating("InvokeQuitController", 1, Interval);
                    }
                }
            }
        }

        public void ExecuteQuitController()
        {
            InvokeQuitController();
        }

        public void InvokeQuitController()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            Debug.Log("Application Quit Execute");
        }
    }
}
