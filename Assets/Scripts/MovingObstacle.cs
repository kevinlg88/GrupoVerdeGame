using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenTeam
{
    public class MovingObstacle : MonoBehaviour
    {
        public float speed;

        [SerializeField] bool destroyAfterCollision = true;

        void Start()
        {
        
        }

        
        void Update()
        {
            if (!GameManager.inst.death && GameManager.inst.isGameRunning) {
                transform.position = transform.position + ( Vector3.left * speed * Time.deltaTime);
                if (transform.position.x < -10.0f) {
                    Destroy(gameObject);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //verifica se colidiu com pilastra, se sim, seta morte como true, inicia anima��o e som de morte.
            if (collision.collider.CompareTag("Player") && destroyAfterCollision) {
                // Debug.Log("Destroy");
                Destroy(gameObject);
                
            }
        }
    }
}
