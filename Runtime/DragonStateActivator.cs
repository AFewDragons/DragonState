using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFewDragons
{
    public class DragonStateActivator : MonoBehaviour
    {
        [SerializeField]
        private DragonStateComparison comparison = new DragonStateComparison();

        [SerializeField]
        private GameObject targetObject;

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
            if (targetObject)
            {
                targetObject.SetActive(comparison.Check());
            }
        }
    }
}