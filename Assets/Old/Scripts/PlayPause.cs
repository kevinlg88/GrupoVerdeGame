
using UnityEngine;
using UnityEngine.UI;

public class PlayPause : MonoBehaviour
{
    [SerializeField]
    private bool pausado = false;
    [SerializeField]
    private Sprite[] playpause;

    public void playPause() {
        //caso pressione o botão de pause, pausa/retorna ao jogo com troca de sprite do botão.
        if (pausado)
        {
            Time.timeScale = 1.0f;
            pausado = false;
            gameObject.GetComponent<Image>().sprite = playpause[0];
        }
        else {
            Time.timeScale = 0;
            pausado = true;
            gameObject.GetComponent<Image>().sprite = playpause[1];
        }

    }
}
