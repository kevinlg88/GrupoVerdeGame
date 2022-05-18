using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenTeam
{
    public class PowerUp : MonoBehaviour
    {
        enum PowerUpType {Coffe, Notification, NewPhone}
        [Tooltip("Qual powerup vai ser")]
        [SerializeField] PowerUpType type = PowerUpType.Coffe;

        [Tooltip("Valor do dash do Coffe")]
        [SerializeField]public float dashValue = 0.2f;

        void Start()
        {
        
        }

        void Update()
        {
            if (!GameManager.inst.isGameRunning || GameManager.inst.isGamePaused)
                return;

            transform.position = transform.position + ( Vector3.left * GameManager.inst.obstaclesSpeed * Time.deltaTime);
            
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
                Destroy(gameObject);
                
            if(!other.gameObject.CompareTag("Player"))
                return;

            if(type == PowerUpType.Coffe)
            {
                StartCoroutine(nameof(CoffeRoutine));
            }
            else if(type == PowerUpType.Notification)
            {
                StartCoroutine(nameof(NotificationRoutine));
            }
            else if(type == PowerUpType.NewPhone)
            {
                StartCoroutine(nameof(NewPhoneRoutine));
            }

        }

        IEnumerator CoffeRoutine()
        {
            GameManager.inst.DashPlayer(-dashValue);
            yield return null;
        }
        IEnumerator NotificationRoutine()
        {

            yield return null;
        }
        IEnumerator NewPhoneRoutine()
        {

            yield return null;
        }


    }
}
