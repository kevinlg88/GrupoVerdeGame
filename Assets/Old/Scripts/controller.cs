
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class controller : MonoBehaviour
{
    [SerializeField]
    private float forcaDoPulo;
    [SerializeField]
    private float forcaDown;
    [SerializeField]
    private PlayerSounds psnds;
    public void Movimento(bool pulando, bool cortapulo)
    {
        if (gameObject.GetComponent<groundcheck>().getEstaNoChao() && pulando)//se estiver no chao e pulando, pula
        {
            gameObject.GetComponent<groundcheck>().setEstanoChao(false);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, forcaDoPulo), ForceMode2D.Impulse);
        }
        if (cortapulo && !gameObject.GetComponent<groundcheck>().getEstaNoChao()) {// se estiver no cortapulo e nao estiver no chao, cai mais rápido
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -forcaDown), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //verifica se colidiu com pilastra, se sim, seta morte como true, inicia animação e som de morte.
        if (collision.collider.CompareTag("pipe")) {
            if (!GameManager.inst.getDeath()){
                GameManager.inst.SetDeath();
                gameObject.GetComponent<Animator>().SetTrigger("death");
                PlayerSounds.inst.sounds[4].Play();
            }
        }
    }


    public void playRun1() {
        PlayerSounds.inst.sounds[0].Play();
    }

    public void playRun2()
    {
        PlayerSounds.inst.sounds[1].Play();
    }

    public void playJump()
    {
        PlayerSounds.inst.sounds[3].Play();
    }

    public void playSlide()
    {
        PlayerSounds.inst.sounds[2].Play();
    }

}
