using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFewDragons
{
    public class DragonStateActivator : MonoBehaviour
    {
        [SerializeField]
        private DragonStateComparisonList comparison;

        [SerializeField]
        private GameObject targetObject;

        private void Awake()
        {
            if (targetObject)
            {
                targetObject.SetActive(comparison.Check());
            }
        }
    }
}