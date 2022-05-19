using System;
using UnityEngine;

namespace GreenTeam
{
    public class PhotographerObstacle : MonoBehaviour
    {
        [SerializeField] bool destroyAfterCollision = true;
        [SerializeField] bool makeItIntangibleAfterCollision = false;
        [SerializeField] bool isALike = false;

        private Action<MovingObstacle> RELEASE_ACTION;

        public void Init(Action<MovingObstacle> releaseAction)
        {
            RELEASE_ACTION = releaseAction;
        }

        void Update()
        {
            if (!GameManager.inst.isGameRunning || GameManager.inst.isGamePaused)
                return;

            transform.position = transform.position + (Vector3.left * GameManager.inst.obstaclesSpeed * Time.deltaTime);

        }

        public virtual void OnCollisionEnter2D(Collision2D collision)
        {
            // Debug.Log(collision.gameObject.name);
            // RELEASE_ACTION(this);

            if (collision.collider.CompareTag("Wall"))
                Destroy(gameObject);

            if (collision.collider.CompareTag("Player"))
            {
                if (destroyAfterCollision)
                    Destroy(gameObject);

                else if (makeItIntangibleAfterCollision)
                {
                    GetComponent<Collider2D>().isTrigger = true;
                    PhotographerFadeOut();
                }
            }
        }

        private void PhotographerFadeOut()
        {
            foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                if (spriteRenderer == null) continue;

                spriteRenderer.color = new Color(0f, 0f, 0f, 0.5f);
            }
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (isALike)
            {
                if (GameManager.inst.isInSineEffect)
                    GameManager.inst.likes += 2;
                else
                    GameManager.inst.likes++;

                Destroy(gameObject);
            }

            if (other.CompareTag("Wall")) Destroy(gameObject);

        }
    }
}
