using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenTeam
{
    public class FanInteraction : MonoBehaviour
    {
        bool isRunning;
        PlayerController playerController;

        int timesTaped;

        void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        void Update()
        {
            if(!isRunning)
                return;
            

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player")) {
                StartCoroutine(InteractionRoutine());
            }
        }

        IEnumerator InteractionRoutine()
        {
            isRunning = true;
            GetComponent<MovingObstacle>().speed = 0;
            
            playerController.animator.SetFloat("velx", 0);
            yield return new WaitUntil( () => CheckTimesTaped(10));
            Destroy(gameObject);
            playerController.animator.SetFloat("velx", 10);
            Debug.Log("FOi");
        }

        bool CheckTimesTaped(float expectedTaps)
        {
            if(playerController.inputs.tap)
                timesTaped++;

            if(timesTaped >= expectedTaps)
            {
                timesTaped = 0;
                return true;
            }
            return false;
        }
    }
}
