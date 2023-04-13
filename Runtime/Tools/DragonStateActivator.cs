using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AFewDragons
{
    public class DragonStateActivator : MonoBehaviour
    {
        [SerializeField]
        private DragonStateComparison comparison;

        [SerializeField]
        [FormerlySerializedAs("targetObject")]
        private GameObject comparisonMetTarget;

        [SerializeField]
        private GameObject comparisonNotMetTarget;

        [SerializeField]
        private DragonStateBase[] stateTriggers; 

        private void Awake()
        {
            Check();
        }

        private void OnEnable()
        {
            foreach (var trigger in stateTriggers)
            {
                trigger.AddUpdateListener(Check);
            }
        }

        private void OnDisable()
        {
            foreach (var trigger in stateTriggers)
            {
                trigger.RemoveUpdateListener(Check);
            }
        }

        public void Check()
        {
            if (comparisonMetTarget)
            {
                comparisonMetTarget.SetActive(comparison.Check());
            }
            if (comparisonNotMetTarget)
            {
                comparisonNotMetTarget.SetActive(!comparison.Check());
            }
        }
    }
}