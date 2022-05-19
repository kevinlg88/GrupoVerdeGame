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

#if UNITY_EDITOR || UNITY_WEBGL
                PCInputs();
#else
                MobileInputs();
#endif
            }

            private void PCInputs()
            {
                if (Input.GetMouseButtonDown(0))
                    moveUp = true;
                else
                    moveUp = false;

                if (Input.GetMouseButtonDown(0))
                    tap = true;
                else
                    tap = false;

                if (Input.GetMouseButtonDown(1))
                    moveDown = true;
                else
                    moveDown = false;
            }

            private void MobileInputs()
            {
                TouchInput.ProcessTouches();

                if (TouchInput.SwipeUp())
                    moveUp = true;
                else
                    moveUp = false;

                if (TouchInput.Tap())
                    tap = true;
                else
                    tap = false;

                if (TouchInput.SwipeDown())
                    moveDown = true;
                else
                    moveDown = false;
            }
        }

        public Inputs inputs = new Inputs();
        #endregion
        [Header("Configs")]
        public float jumpForce;
        public float getDownForce;
        [SerializeField] bool canDoubleJump = false;

        [Tooltip("Porcentagem que o player irá perder ao colidir com um obstaculo")]
        [SerializeField] float _percentageToLost = 0.05f;

        public float percentageToLost { get => _percentageToLost; }

        [SerializeField] LayerMask groundLayer;

        [SerializeField] float loseSpeedMultiplier = 1f;

        bool isOnGround = false;

        bool isSliding = false;

        bool isDead = false;

        float nbJumps = 0;

        [Range(0, 1)] private float _playerXPositionPercentage;

        public float playerXPositionPercentage{ 
            get => _playerXPositionPercentage;
            set {
                _currentTime = 0f;
                _playerXPositionPercentage = value;
            }
        }

        [Header("References")]

        [SerializeField] Transform footPosition;
        [SerializeField] Transform losePosition;

        Vector3 initialPosition;

        Rigidbody2D rb;

        [SerializeField] Animator _animator;

        public Animator animator { get => _animator; }

        void Awake()
        {
            initialPosition = transform.position;
            rb = GetComponent<Rigidbody2D>();
            // _animator = GetComponent<Animator>();
        }
        void Start()
        {
            _animator.SetFloat("velx", 0);
            GameManager.inst.ON_START_GAME += () => _animator.SetFloat("velx", 1);
            GameManager.inst.ON_START_GAME += () => _animator.SetBool("isGameRuning", true);
            GameManager.inst.ON_END_GAME += () => _animator.SetBool("isGameRuning", false);
        }

        void Update()
        {
            if (!GameManager.inst.isGameRunning || GameManager.inst.isGamePaused)
                return;

            IsOnGroundCheck();
            CheckIfLose();
            inputs.UpdateInputs();
            Movement();

            isSliding = animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"); //if is sliding
            
            if (playerXPositionPercentage < -0.6f)
                playerXPositionPercentage = -0.6f;

        }

        void FixedUpdate()
        {
            if (isDead) return;

            _animator.SetFloat("vely", rb.velocity.y);

            _playerXPositionPercentage += Time.deltaTime * loseSpeedMultiplier;

            ChangeXPosition(initialPosition.x + (losePosition.position.x * playerXPositionPercentage));
        }

        private bool CanDoubleJump() => nbJumps < 1 && canDoubleJump;

        void Movement()
        {
            if (!GameManager.inst.isGameRunning || GameManager.inst.isInFanInteraction)
                return;

            if (inputs.moveUp)
            {
                if (isOnGround || CanDoubleJump())
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.AddForce(new Vector2(0f, jumpForce * 45), ForceMode2D.Force);
                    nbJumps++;
                }
            }

            if (inputs.moveDown)
            {
                if (!isOnGround)
                {
                    rb.AddForce(new Vector2(0f, -getDownForce), ForceMode2D.Impulse);
                    // else if (!isSliding)
                }
                _animator.SetTrigger("slide");
            }
        }


        void IsOnGroundCheck()
        {
            // RaycastHit2D hit2D = Physics2D.Raycast(rb.position - new Vector2(0f, 0.5f), Vector2.down, 0.2f, groundLayer);
            // isOnGround = hit2D;
            isOnGround = Physics2D.OverlapCircle(footPosition.position, 0.05f, groundLayer);
            if (isOnGround)
                _animator.SetBool("estanochao", true);
            else
                _animator.SetBool("estanochao", false);

            if (isOnGround)
                nbJumps = 0;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //verifica se colidiu com pilastra, se sim, seta morte como true, inicia anima��o e som de morte.
            if (collision.collider.CompareTag("MovingObstacles"))
            {
                _currentTime = 0f;
                playerXPositionPercentage += _percentageToLost;
            }
        }

        /// <summary>
        /// This property controls the lerp of the playerXPositionPercentage.
        /// </summary>
        float _currentTime = 0f;
        void ChangeXPosition(float newXPosition)
        {
            if (!GameManager.inst.isGameRunning)
                return;

            if(_currentTime >= .5f)
                _currentTime = 0f;
            
            _currentTime += Time.deltaTime;

            transform.position = new Vector3(Mathf.Lerp(transform.position.x, newXPosition, _currentTime), transform.position.y, transform.position.z);
            
        }

        void CheckIfLose()
        {

            if (playerXPositionPercentage >= 1)
                if (!GameManager.inst.death)
                {
                    isDead = true;
                    GameManager.inst.death = true;
                    animator.SetTrigger("death");
                    GameManager.inst.audioManager.death.Play();
                    _animator.SetFloat("velx", 0);
                }
        }

        public void playRun1()
        {
            GameManager.inst.audioManager.run.Play();
        }

        public void playRun2()
        {

        }

        public void playJump()
        {
            GameManager.inst.audioManager.jump.Play();
        }

        public void playSlide()
        {
            GameManager.inst.audioManager.slide.Play();
        }

        public void SlideStart()
        {
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
