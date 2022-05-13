using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenTeam
{
    public class Like : MonoBehaviour
    {

        void Start()
        {
        
        }

        void Update()
        {
        
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            GameManager.inst.likes++;
            Destroy(gameObject);
            
        }
    }
}
