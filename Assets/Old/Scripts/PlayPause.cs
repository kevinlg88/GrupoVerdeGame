
using UnityEngine;
using UnityEngine.UI;
using GreenTeam;

public class PlayPause : MonoBehaviour
{
    [SerializeField]
    private bool pausado = false;
    [SerializeField]
    // private Sprite[] playpause;

    public void playPause() {
        //caso pressione o bot�o de pause, pausa/retorna ao jogo com troca de sprite do bot�o.
        if (GameManager.inst.isGamePaused)
        {
            Time.timeScale = 1.0f;
            GameManager.inst.isGamePaused = false;
            // gameObject.GetComponent<Image>().sprite = playpause[0];
        }
        else {
            Time.timeScale = 0;
            GameManager.inst.isGamePaused = true;
            // gameObject.GetComponent<Image>().sprite = playpause[1];
        }
    }
}
