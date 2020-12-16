/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk membuat global transfer antara global variabel
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zetcil
{
    public class TransferController : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Global Variable")]
        public GlobalVariable.CVariableType VariableType;
        public VarTime TimeSender;
        public VarTime TimeReceiver;
        public VarHealth HealthSender;
        public VarHealth HealthReceiver;
        public VarMana ManaSender;
        public VarMana ManaReceiver;
        public VarExp ExpSender;
        public VarExp ExpReceiver;
        public VarScore ScoreSender;
        public VarScore ScoreReceiver;
        public VarInteger IntSender;
        public VarInteger IntReceiver;
        public VarFloat FloatSender;
        public VarFloat FloatReceiver;
        public VarString StringSender;
        public VarString StringReceiver;
        public VarBoolean BoolSender;
        public VarBoolean BoolReceiver;

        [Header("Increment Settings")]
        public float Increment;

        [System.Serializable]
        public class CAttributeSettings{
            public float MinValue;
            public UnityEvent AttributeEvent;
        }

        [Header("Attribue Settings")]
        public bool usingCAttributeSettings;
        public CAttributeSettings[] AttributeSettings;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (VariableType == GlobalVariable.CVariableType.scoreVar)
                {
                    if (ScoreReceiver.CurrentValue < ScoreSender.CurrentValue)
                    {
                        ScoreReceiver.AddToCurrentValue(Increment + 1);
                    }
                    if (ScoreReceiver.CurrentValue >= ScoreSender.CurrentValue)
                    {
                        ScoreReceiver.CurrentValue = ScoreSender.CurrentValue;

                        if (usingCAttributeSettings)
                        {
                            for (int i = 0; i < AttributeSettings.Length; i++)
                            {
                                if (ScoreReceiver.CurrentValue >= AttributeSettings[i].MinValue) {
                                    AttributeSettings[i].AttributeEvent.Invoke();
                                }
                            }
                        }

                    }
                }
            }
        }

    }
}
