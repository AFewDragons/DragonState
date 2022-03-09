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

        private void Awake()
        {
            if (targetObject)
            {
                targetObject.SetActive(comparison.Check());
            }
        }
    }
}