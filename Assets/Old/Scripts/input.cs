
using UnityEngine;

public class input : MonoBehaviour
{
    private Touch initTouch; 
    private bool swiped = false;
    [SerializeField]
    private bool isSlide;


    void FixedUpdate()
    {
        if (!GameManager.inst.getDeath() && GameManager.inst.getInit())//caso o personagem n�o tenha morrido e tenha iniciado o jogo
        {
            gameObject.GetComponent<Animator>().SetFloat("velx", 10);// come�ar a correr
            if (Input.GetMouseButton(1))
            {
                if (gameObject.GetComponent<groundcheck>().getEstaNoChao()&&!isSlide) { 
                    
                    gameObject.GetComponent<Animator>().SetTrigger("slide");//efetua o slide
                }
                else
                {
                    gameObject.GetComponent<controller>().Movimento(false, true);//cai no ch�o mais r�pido
                }
            }
            if (Input.GetMouseButton(0) && gameObject.GetComponent<groundcheck>().getEstaNoChao()) //caso aperte o botao de pular e esteja no chao
            {
                SlideEnd();//cancela o slide
                gameObject.GetComponent<controller>().Movimento(true, false); //efetuar o impulso do pulo
            }
            Movimentar(); //controle caso seja importado para android
            //seta as vari�veis no animator
            gameObject.GetComponent<Animator>().SetFloat("vely", gameObject.GetComponent<Rigidbody2D>().velocity.y);
            gameObject.GetComponent<Animator>().SetBool("estanochao", gameObject.GetComponent<groundcheck>().getEstaNoChao());
        }
        else {
            gameObject.GetComponent<Animator>().SetFloat("velx", 0);//caso tenha morrido, ou n�o tenha iniciado a anima��o seta a velx para 0
        }
    }
    void Movimentar()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began) //se o touch inicial acontecer
            {
                initTouch = t;
            }
            else if ((t.phase == TouchPhase.Moved)&& !swiped)//verifica se o usu�rio moveu o dedo pela tela
            {
                float yMoved = initTouch.position.y - t.position.y;//verifica quanto foi movido em y
                float distance = Mathf.Sqrt((yMoved * yMoved)); // calcula a dist�ncia (h2 = p2 + b2)

                if (distance > 25f)//caso a dist�ncia for maior que 25, consideramos como um comando
                {
                    if (yMoved < 0 && gameObject.GetComponent<groundcheck>().getEstaNoChao()) // efetua o pulo se moveu para cima
                    {
                        SlideEnd();
                        gameObject.GetComponent<controller>().Movimento(true, false);
                    }
                    else if (yMoved > 0)// verifica se moveu para cima
                    {
                        if (gameObject.GetComponent<groundcheck>().getEstaNoChao() && !isSlide)
                        {
                            gameObject.GetComponent<Animator>().SetTrigger("slide");//efetua o slide
                        }
                        else
                        {
                            gameObject.GetComponent<controller>().Movimento(false, true);// cai no ch�o mais r�pido
                        }
                    }
                    swiped = true;//marca como final do movimento, caso queira ter um novo, necessario de swiped ser false
                }
            }
            else if (t.phase == TouchPhase.Ended)// quando o touch se encerrar
            {
                initTouch = new Touch();
                swiped = false;
            }
        }
    }

    public void SlideINIT() {
        isSlide = true;
    }
    public void SlideEnd()
    {
        isSlide = false;
    }
}
