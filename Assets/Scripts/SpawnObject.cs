using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenTeam
{
    public class SpawnObject : MonoBehaviour
    {
        [SerializeField] List<GameObject> objects = new List<GameObject>();
        public float maxTime = 3;
        public float minTime = 0;
        private float time;//current time
        private float spawnTime;//The time to spawn the object

        void Start()
        {
        
        }

        void Update()
        {
            if(!GameManager.inst.isGameRunning)
                return;

            //Counts up
            time += Time.deltaTime;
            //Check if its the right time to spawn the object
            int index = Random.Range(0, objects.Count);
            Vector3 position = new Vector3(transform.position.x, Random.Range(4.15f, 10.5f), 0);
            Debug.Log(index);
            if (time >= spawnTime) {
                if(!GameManager.inst.isInFanInteraction)
                    Instantiate(objects[index], position, transform.rotation);

                SetRandomTime();
                time = 0;
            }
        }

         void SetRandomTime ()
        {
            spawnTime = Random.Range (minTime, maxTime);
            // Debug.Log ("Next object spawn in " + spawnTime + " seconds.");
        }
    }
}
