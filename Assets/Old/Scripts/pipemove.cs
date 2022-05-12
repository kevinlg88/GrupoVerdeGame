
using UnityEngine;

public class pipemove : MonoBehaviour
{
    [SerializeField]
    private float velocidade;
    
    void Update()
    {
        //caso o player n�o tenha morrido e tenha iniciado o jogo, pilastras instanciadas se movem
        // if (!GameManager.inst.getDeath() && GameManager.inst.getInit()) {
        //     transform.position = transform.position + ( Vector3.left * velocidade * Time.deltaTime);
        //     if (transform.position.x < -8.0f) {
        //         Destroy(gameObject);
        //     }
        // }


    }
}
