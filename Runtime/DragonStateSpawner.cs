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
        private DragonStateComparison prerequisiteState;

        [SerializeField]
        private DragonStateComparison completedState;

        [Header("Area")]
        [SerializeField]
        private GameObject beforePrereqsPrefab;

        [SerializeField]
        private GameObject completedStatePrefab;

        [SerializeField]
        private GameObject spawnPrefab;

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