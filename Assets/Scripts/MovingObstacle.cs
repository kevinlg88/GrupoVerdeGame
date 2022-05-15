using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenTeam
{
    public class MovingObstacle : MonoBehaviour
    {
        // public float speed;
        [SerializeField] bool usePool = true;
        [SerializeField] bool destroyAfterCollision = true;
        [SerializeField] bool makeItIntangibleAfterCollision = false;

        private Action<MovingObstacle> RELEASE_ACTION;

        public void Init(Action<MovingObstacle> releaseAction)
        {
            RELEASE_ACTION = releaseAction;
        }

        void Start()
        {
        
        }

        
        void Update()
        {
            if (!GameManager.inst.isGameRunning || GameManager.inst.isGamePaused)
                return;

            transform.position = transform.position + ( Vector3.left * GameManager.inst.obstaclesSpeed * Time.deltaTime);
            
        }

        public virtual void OnCollisionEnter2D(Collision2D collision)
        {
            // Debug.Log(collision.gameObject.name);
            // RELEASE_ACTION(this);
            if (collision.collider.CompareTag("Wall"))
                if(usePool)
                    RELEASE_ACTION(this);
                else
                    Destroy(gameObject);

            if (collision.collider.CompareTag("Player")) {
                if(destroyAfterCollision)
                    if(usePool)
                        RELEASE_ACTION(this);
                    else
                        Destroy(gameObject);
                    
                else if(makeItIntangibleAfterCollision)
                {
                    GetComponent<Collider2D>().isTrigger = true;
                    GetComponent<SpriteRenderer>().color = new Color(0f,0f,0f,0.5f);
                }
            }
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if(usePool)
            {
                if (other.CompareTag("Wall")) RELEASE_ACTION(this);

            }
            else
            {
                if (other.CompareTag("Wall")) Destroy(gameObject);

            }
        }
    }
}
