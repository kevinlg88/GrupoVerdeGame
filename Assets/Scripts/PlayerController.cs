using System.Security.AccessControl;
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
            public bool tap;

            public void UpdateInputs()
            {
                if(Input.GetMouseButtonDown(0) || TouchInput.SwipeUp())
                    moveUp = true;
                else
                    moveUp = false;


                if(Input.GetMouseButtonDown(0) || TouchInput.Tap())
                    tap = true;
                else
                    tap = false;

                if(Input.GetMouseButtonDown(1) || TouchInput.SwipeDown())
                    moveDown = true;
                else
                    moveDown = false;

                // Debug.Log(Input.GetMouseButtonDown(0));
            }

        }
        public Inputs inputs = new Inputs();
        #endregion
        [Header("Configs")]
        public float jumpForce;
        public float getDownForce;

        [Tooltip("Porcentagem que o player irá perder ao colidir com um obstaculo")]
        [SerializeField] float percentageToLost = 0.05f;

        [SerializeField] LayerMask groundLayer;

        [SerializeField] float loseSpeedMultiplier = 1f;

        bool isOnGround = false;

        bool isSliding = false;

        bool isDead = false;

        float nbJumps = 0;

        [Range(0,1)] public float playerXPositionPercentage;

        [Header("References")]

        [SerializeField] Transform footPosition;
        [SerializeField] Transform losePosition;

        Vector3 initialPosition;

        Rigidbody2D rb;

        Animator _animator;

        public Animator animator{ get => _animator; }

        void Awake()
        {
            initialPosition = transform.position;
            rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
        void Start()
        {
            _animator.SetFloat("velx", 0);
            GameManager.inst.ON_START_GAME += () => _animator.SetFloat("velx", 1);
        }
        void Update()
        {
            if(!GameManager.inst.isGameRunning || GameManager.inst.isGamePaused)
                return;
                
            if(playerXPositionPercentage < -0.6f)
                playerXPositionPercentage = -0.6f;

            playerXPositionPercentage += Time.deltaTime * loseSpeedMultiplier;
            IsOnGroundCheck();
            CheckIfLose();
            inputs.UpdateInputs();
            Movement();
            

        }
        void FixedUpdate()
        {
            if(isDead)
            return;
            
            SetXPosition();
        }

        void Movement()
        {
            if(!GameManager.inst.isGameRunning || GameManager.inst.isInFanInteraction)
                return;

            if (inputs.moveUp)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                if(isOnGround)
                {
                    rb.AddForce(new Vector2(0f, jumpForce*45), ForceMode2D.Force);
                    nbJumps++;
                }
                else if(nbJumps < 1)
                {
                    rb.AddForce(new Vector2(0f, jumpForce*45), ForceMode2D.Force);
                    nbJumps++;
                }
            }

            if (inputs.moveDown) {
                if(!isOnGround)
                    rb.AddForce(new Vector2(0f, -getDownForce), ForceMode2D.Impulse);
                else if(!isSliding)
                    _animator.SetTrigger("slide");

            }
        }

        void IsOnGroundCheck()
        {
            // RaycastHit2D hit2D = Physics2D.Raycast(rb.position - new Vector2(0f, 0.5f), Vector2.down, 0.2f, groundLayer);
            // isOnGround = hit2D;
            isOnGround = Physics2D.OverlapCircle(footPosition.position, 0.05f, groundLayer);
            if(isOnGround)
                nbJumps = 0;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //verifica se colidiu com pilastra, se sim, seta morte como true, inicia anima��o e som de morte.
            if (collision.collider.CompareTag("MovingObstacles")) {
                
                playerXPositionPercentage += Mathf.Lerp(0, percentageToLost, 1f);
            }
        }

        void SetXPosition()
        {
            if(!GameManager.inst.isGameRunning)
                return;

            float newXPosition = initialPosition.x + (losePosition.position.x * playerXPositionPercentage);
            // Debug.Log(newXPosition);
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }

        void CheckIfLose()
        {
            
            if(playerXPositionPercentage >= 1)
                if (!GameManager.inst.death){
                    isDead = true;
                    GameManager.inst.death = true;
                    gameObject.GetComponent<Animator>().SetTrigger("death");
                    PlayerSounds.inst.sounds[4].Play();
                    _animator.SetFloat("velx", 0);
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
