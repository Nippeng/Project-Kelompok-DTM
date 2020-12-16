using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class VarTransform : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;

        [Header("Transform Settings")]
        public Transform TargetTransform;

        [Header("Position Settings")]
        public bool usingPosition;
        [ConditionalField("usingPosition")] public VarFloat PositionX;
        [ConditionalField("usingPosition")] public VarFloat PositionY;
        [ConditionalField("usingPosition")] public VarFloat PositionZ;

        [Header("Rotation Settings")]
        public bool usingRotation;
        [ConditionalField("usingRotation")] public VarFloat RotationX;
        [ConditionalField("usingRotation")] public VarFloat RotationY;
        [ConditionalField("usingRotation")] public VarFloat RotationZ;

        [Header("Scale Settings")]
        public bool usingScale;
        [ConditionalField("usingScale")] public VarFloat ScaleX;
        [ConditionalField("usingScale")] public VarFloat ScaleY;
        [ConditionalField("usingScale")] public VarFloat ScaleZ;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (usingPosition)
                {
                    PositionX.CurrentValue = TargetTransform.position.x;
                    PositionY.CurrentValue = TargetTransform.position.y;
                    PositionZ.CurrentValue = TargetTransform.position.z;
                }
                if (usingRotation)
                {
                    RotationX.CurrentValue = TargetTransform.localEulerAngles.x;
                    RotationY.CurrentValue = TargetTransform.localEulerAngles.y;
                    RotationZ.CurrentValue = TargetTransform.localEulerAngles.z;
                }
                if (usingScale)
                {
                    ScaleX.CurrentValue = TargetTransform.localScale.x;
                    ScaleY.CurrentValue = TargetTransform.localScale.y;
                    ScaleZ.CurrentValue = TargetTransform.localScale.z;
                }
            }
        }
    }
}

