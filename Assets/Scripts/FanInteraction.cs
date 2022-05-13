using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreenTeam
{
    public class FanInteraction : MonoBehaviour
    {
        bool isRunning;
        PlayerController playerController;

        GameObject uiObject;

        int timesTaped;

        void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
            uiObject = GameObject.Find("Interagindo Fans");
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
            
            GameManager.inst.slowPercentage = 0.5f;
            GameManager.inst.isInFanInteraction = true;
            playerController.animator.SetFloat("velx", 0.5f);
            uiObject.GetComponent<Text>().text = "Toque varias vezes na tela";


            yield return new WaitUntil( () => CheckTimesTaped(10));
            
            GetComponent<Collider2D>().isTrigger = true;
            GetComponent<SpriteRenderer>().color = new Color(0f,0f,0f,0.5f);
            
            GameManager.inst.isInFanInteraction = false;
            uiObject.GetComponent<Text>().text = "";
            GameManager.inst.slowPercentage = 1f;
            // Destroy(gameObject);
            playerController.animator.SetFloat("velx", 10);
            GameManager.inst.likes += 5;
            
            GetComponent<MovingObstacle>().speed = 4;
            
            playerController.playerXPositionPercentage -= Mathf.Lerp(0, 0.30f, 1f);
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
