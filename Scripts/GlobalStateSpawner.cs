using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AFewDragons
{
    public class GlobalStateSpawner : MonoBehaviour
    {
        [Header("State")]
        [SerializeField]
        private GlobalStateComparison prerequisiteState;

        [SerializeField]
        private GlobalStateComparison completedState;

        [Header("Area")]
        [SerializeField]
        private GameObject beforePrereqsPrefab;

        [SerializeField]
        private GameObject completedStatePrefab;

        [SerializeField]
        private GameObject spawnPrefab;

        private void OnEnable()
        {
            GlobalStateManager.AddListener(OnEvent);
        }

        private void OnDisable()
        {
            GlobalStateManager.RemoveListener(OnEvent);
        }

        // Start is called before the first frame update
        void Start()
        {
            Resolve();
        }

        private void OnEvent(string key, object value)
        {
            //Resolve();
        }

        private void Resolve()
        {
            if (prerequisiteState.Check())
            {
                if (!completedState.Check())
                {
                    if(spawnPrefab)
                        Instantiate(spawnPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    if (completedStatePrefab)
                        Instantiate(completedStatePrefab, transform.position, Quaternion.identity);
                }
            }
            else
            {
                if (beforePrereqsPrefab)
                    Instantiate(beforePrereqsPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}