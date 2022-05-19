using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenTeam
{
    public class GetCollision : MonoBehaviour
    {
        [SerializeField] PlayerController player;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //verifica se colidiu com pilastra, se sim, seta morte como true, inicia anima��o e som de morte.
            if (collision.collider.CompareTag("MovingObstacles")) {
                // player.playerXPositionPercentage += Mathf.Lerp(0, player.percentageToLost, 1f);
                GameManager.inst.ChangeXPlayer(player.percentageToLost);
            }
        }
    }
}
