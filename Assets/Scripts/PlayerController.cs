using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenTeam
{
    public class PlayerController : MonoBehaviour
    {
        #region INPUT
        [System.Serializable]
        public class Inputs
        {
            public bool moveUp;
            public bool moveDown;

            public void UpdateInputs()
            {
                moveUp = Input.GetMouseButton(0);
                moveDown = Input.GetMouseButton(1);
            }

        }
        [SerializeField] Inputs inputs = new Inputs();
        #endregion
        [Header("Configs")]
        public float jumpForce;
        public float getDownForce;

        [SerializeField] LayerMask groundLayer;

        bool isOnGround = false;

        bool isSliding = false;

        bool isDead = false;

        [SerializeField]
        [Range(0,1)] float playerXPositionPercentage;

        [Header("References")]

        [SerializeField] Transform footPosition;
        [SerializeField] Transform losePosition;

        Vector3 initialPosition;

        Rigidbody2D rb;

        Animator animator;

        void Awake()
        {
            initialPosition = transform.position;
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
        void Start()
        {
            animator.SetFloat("velx", 10);
        }
        void Update()
        {
            inputs.UpdateInputs();
            IsOnGroundCheck();
        }
        void FixedUpdate()
        {
            SetXPosition();
            Movement();
        }

        void Movement()
        {
            if (inputs.moveUp && isOnGround)
            {
                // isOnGround = false;
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }

            if (inputs.moveDown) {
                if(!isOnGround)
                    rb.AddForce(new Vector2(0f, -getDownForce), ForceMode2D.Impulse);
                else if(!isSliding)
                    animator.SetTrigger("slide");

            }
        }

        void IsOnGroundCheck()
        {
            // RaycastHit2D hit2D = Physics2D.Raycast(rb.position - new Vector2(0f, 0.5f), Vector2.down, 0.2f, groundLayer);
            // isOnGround = hit2D;
            isOnGround = Physics2D.OverlapCircle(footPosition.position, 0.05f, groundLayer);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //verifica se colidiu com pilastra, se sim, seta morte como true, inicia anima��o e som de morte.
            if (collision.collider.CompareTag("pipe")) {
                if (!GameManager.inst.getDeath()){
                    isDead = true;
                    GameManager.inst.SetDeath();
                    gameObject.GetComponent<Animator>().SetTrigger("death");
                    PlayerSounds.inst.sounds[4].Play();
                    animator.SetFloat("velx", 0);//caso tenha morrido, ou n�o tenha iniciado a anima��o seta a velx para 0
                }
            }
        }

        void SetXPosition()
        {
            float newXPosition = initialPosition.x + (losePosition.position.x * playerXPositionPercentage) ;
            // Debug.Log(newXPosition);
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
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

        public void SlideINIT() {
            isSliding = true;
        }
        public void SlideEnd()
        {
            isSliding = false;
        }

        private void OnDrawGizmos()//DEBUG
        {
            //apenas para que possamos ver o circulo
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(footPosition.position, 0.05f);
        }
    }
}
