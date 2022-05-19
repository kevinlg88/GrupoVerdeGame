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
        
        [Tooltip("Se usa o blocos de prefabs")]
        [SerializeField] private bool useBlocks;

        void Start()
        {
        
        }

        void Update()
        {
            if(!GameManager.inst.isGameRunning)
                return;

            //Counts up
            if(!GameManager.inst.isInFanInteraction)
                time += Time.deltaTime;


            //Check if its the right time to spawn the object
            if (time >= spawnTime) {
                if(!GameManager.inst.isInFanInteraction)
                    Spawn();

                SetRandomTime();
                time = 0;
            }
        }

        void SetRandomTime ()
        {
            spawnTime = Random.Range (minTime, maxTime);
            // Debug.Log ("Next object spawn in " + spawnTime + " seconds.");
        }

        void Spawn()
        {
            int index = Random.Range(0, objects.Count);
            // Debug.Log(index);

            Vector3 position = new Vector3(transform.position.x, Random.Range(4.15f, 10.5f), 0);
            if(useBlocks)
                Instantiate(objects[index]);
            else
                Instantiate(objects[index], position, transform.rotation);
        }
    }
}
