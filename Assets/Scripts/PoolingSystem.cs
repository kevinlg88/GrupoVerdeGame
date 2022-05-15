using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace GreenTeam
{
    public class PoolingSystem : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            [SerializeField] MovingObstacle objectToSpawn;
            // [SerializeField] int spawnAmount = 2;
            [SerializeField] int defaultAmount = 2;
            [SerializeField] int maxAmount = 10;
            
            public ObjectPool<MovingObstacle> pool;

            public void InitializePool(Vector3 defaultPosition)
            {
                pool = new ObjectPool<MovingObstacle>(() => {
                return Instantiate(objectToSpawn);
                }, obj => {
                    obj.gameObject.SetActive(true);
                    // obj.transform.position = defaultPosition;
                    obj.Init(ReleaseObj);
                }, obj => {
                    obj.gameObject.SetActive(false);
                }, obj => {
                    Destroy(obj);
                }, false, defaultAmount, maxAmount);
            }

             void ReleaseObj(MovingObstacle obj)
            {
                pool.Release(obj);
            }
        }

        [SerializeField] List<Pool> poolsList = new List<Pool>();

        public float maxTime = 3;
        public float minTime = 0;
        private float time;//current time
        private float spawnTime;//The time to spawn the object

        // [SerializeField] MovingObstacle objectToSpawn;
        // [SerializeField] int spawnAmount;
        // [SerializeField] int defaultAmount;
        // [SerializeField] int maxAmount;
        
        // private ObjectPool<MovingObstacle> _pool;


        void Start()
        {
            foreach (Pool pl in poolsList)//Inicializa todas as Pools
            {
                pl.InitializePool(transform.position);
            }


            // _pool = new ObjectPool<MovingObstacle>(() => {
            //     return Instantiate(objectToSpawn);
            // }, obj => {
            //     obj.gameObject.SetActive(true);
            //     obj.transform.position = transform.position;
            //     obj.Init(ReleaseObj);
            // }, obj => {
            //     obj.gameObject.SetActive(false);
            // }, obj => {
            //     Destroy(obj);
            // }, false, defaultAmount, maxAmount);
            // InvokeRepeating(nameof(Spawn), 0.5f, 2f);
        }

        void Update()
        {
            if(!GameManager.inst.isGameRunning)
                return;

            
        }

        void FixedUpdate ()
        {
            if(!GameManager.inst.isGameRunning)
                return;

            //Counts up
            time += Time.deltaTime;
            //Check if its the right time to spawn the object
            int index = Random.Range(0, poolsList.Count);
            
            Debug.Log(index);
            if (time >= spawnTime) {
                if(!GameManager.inst.isInFanInteraction)
                    Spawn(poolsList[index].pool, transform.position);

                SetRandomTime ();
                time = 0;
            }
        }

        void Spawn(ObjectPool<MovingObstacle> pl, Vector3 position)
        {
            
            
            MovingObstacle obj = pl.Get();
            // obj.gameObject.transform.position = position;
            // MovingObstacle obj = _pool.Get();
                
                
              
        }

        void SetRandomTime ()
        {
            spawnTime = Random.Range (minTime, maxTime);
            // Debug.Log ("Next object spawn in " + spawnTime + " seconds.");
        }
        // void ReleaseObj(MovingObstacle obj)
        // {
        //     _pool.Release(obj);
        // }
    }
}
