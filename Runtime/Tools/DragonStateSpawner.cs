using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AFewDragons
{
    public class DragonStateSpawner : MonoBehaviour
    {
        [Header("State")]
        [SerializeField]
        private DragonStateComparison comparison;

        [SerializeField]
        private bool updateOnStateChange = true;

        [Header("Area")]
        [SerializeField]
        private GameObject conditionsMetPrefab;

        [SerializeField]
        private GameObject conditionsNotMetPrefab;

        private void OnEnable()
        {
            DragonStateManager.AddListener(OnEvent);
        }

        private void OnDisable()
        {
            DragonStateManager.RemoveListener(OnEvent);
        }

        // Start is called before the first frame update
        void Start()
        {
            Resolve();
        }

        private void OnEvent(string key, object value)
        {
            if(updateOnStateChange)
            {
                //Resolve();
            }
        }

        private void Resolve()
        {
            if (comparison.Check())
            {
                if(conditionsMetPrefab)
                    Instantiate(conditionsMetPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                if (conditionsNotMetPrefab)
                    Instantiate(conditionsNotMetPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}