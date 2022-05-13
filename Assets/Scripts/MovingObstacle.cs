using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenTeam
{
    public class MovingObstacle : MonoBehaviour
    {
        public float speed;

        [SerializeField] bool destroyAfterCollision = true;
        [SerializeField] bool makeItIntangibleAfterCollision = false;
        [SerializeField] bool aaaa = true;//destruir se tiver interagindo com fan



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

            if(aaaa && GameManager.inst.isInFanInteraction)
                Destroy(gameObject);

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //verifica se colidiu com pilastra, se sim, seta morte como true, inicia anima��o e som de morte.
            if (collision.collider.CompareTag("Player")) {
                if(destroyAfterCollision)
                    Destroy(gameObject);
                else if(makeItIntangibleAfterCollision)
                {
                    GetComponent<Collider2D>().isTrigger = true;
                    GetComponent<SpriteRenderer>().color = new Color(0f,0f,0f,0.5f);
                }
            }
        }
    }
}
