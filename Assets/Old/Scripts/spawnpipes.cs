
using UnityEngine;
using GreenTeam;

public class spawnpipes : MonoBehaviour
{
    [SerializeField]
    private float[] intervaloDeTempo;
    [SerializeField]
    private GameObject[] pipes;
    [SerializeField]
    private int pipeSorteado;
    [SerializeField]
    private float tempoDecorrido = 0f, tempoSorteado = 3f;

    void Update()
    {
        // caso o player n�o tenha morrido e tenha iniciado o jogo:
        // instancia uma pilastra aleat�ria em um determinado tempo aleat�rio
        if (!GameManager.inst.death && GameManager.inst.isGameRunning && !GameManager.inst.isInFanInteraction)
        {
            tempoDecorrido += Time.deltaTime;
            if (tempoDecorrido >= tempoSorteado)
            {
                if (Random.Range(1, 3) == 1f)
                {
                    pipeSorteado = Random.Range(0, 5);
                    Debug.Log(pipeSorteado);
                    Instantiate(pipes[pipeSorteado]);
                    tempoSorteado = Random.Range(intervaloDeTempo[0], intervaloDeTempo[1]);
                }
                else
                {
                    Instantiate(pipes[5]);
                    tempoSorteado = Random.Range(intervaloDeTempo[0], intervaloDeTempo[1]);
                }
                tempoDecorrido = 0;
            }
        }
    }

}
