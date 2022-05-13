using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreenTeam;

public class addscore : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer spr;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //adiciona score ao game manager caso o colisor de score colida com o player
        if (collision.gameObject.CompareTag("Player")) {
            GameManager.inst.addScore();
            spr.color = Color.green;
            PlayerSounds.inst.sounds[5].Play();
        }
    }
}
