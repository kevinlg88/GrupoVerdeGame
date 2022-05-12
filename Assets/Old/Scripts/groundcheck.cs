using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundcheck : MonoBehaviour
{
    [SerializeField]
    private LayerMask lmChao;     [SerializeField]
    private Transform posicaoDoPe; 
    [SerializeField]
    private float tamanhoDoRaioDoPe;
    [SerializeField]
    private bool estanochao = true;

    private void FixedUpdate()
    {
        //retorna o número de objetos colididos com a layer (lmChao) de Um circulo de raio de tamanho(tamanhoDoRaioDoPe) 
        //centralizado em (posicaoDoPe.position) caso seja null(false), caso não seja null = true;
        estanochao = Physics2D.OverlapCircle(posicaoDoPe.position, tamanhoDoRaioDoPe, lmChao);
    }

    private void OnDrawGizmos()
    {
        //apenas para que possamos ver o circulo
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(posicaoDoPe.position, tamanhoDoRaioDoPe);
    }

    public bool getEstaNoChao() {
        return estanochao;
    }
    public void setEstanoChao(bool p) {
        estanochao = p;
    }
}
