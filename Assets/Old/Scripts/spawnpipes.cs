
using UnityEngine;

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
        //caso o player n�o tenha morrido e tenha iniciado o jogo:
        // instancia uma pilastra aleat�ria em um determinado tempo aleat�rio
        if (!GameManager.inst.getDeath() && GameManager.inst.isGameRunning)
        {
            tempoDecorrido += Time.deltaTime;
            if (tempoDecorrido >= tempoSorteado)
            {
                if (Random.Range(1, 3) == 1f)
                {
                    pipeSorteado = Random.Range(0, 3);
                    Instantiate(pipes[pipeSorteado]);
                    tempoSorteado = Random.Range(intervaloDeTempo[0], intervaloDeTempo[1]);
                }
                else
                {
                    Instantiate(pipes[3]);
                    tempoSorteado = Random.Range(intervaloDeTempo[0], intervaloDeTempo[1]);
                }
                tempoDecorrido = 0;
            }
        }
    }

}
