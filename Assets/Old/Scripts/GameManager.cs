using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GreenTeam
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private int score = 0;
        [SerializeField]
        private bool _isGameRunning = false;
        private bool _isGamePaused = false;
        private bool _death = false;
        public bool isInFanInteraction = false;

        [Tooltip("Dificuldade inicial do jogo. 1f = 100%")]
        [SerializeField] float initialDificult = 1f;

        [Tooltip("Dificuldade maxima do jogo. 1f = 200%")]
        [SerializeField] float maxDificult = 2f;
        float _currentDificult;

        [Tooltip("Define o quão rapido o a dificuldade vai aumentar")]
        [SerializeField] float difcicultMultiplier = 1f;
        private float _travelledDinstance;
        
        private float travelledDinstanceMultiplier = 1f;
        private float _likes;

        [Tooltip("Velocidade inicial dos obstaculos")]
        public float initialObstaclesSpeed = 4f;

        [Tooltip("Velocidade atual dos obstaculos")]
        public float obstaclesSpeed = 4f;

        // [Tooltip("Valor do dash do Coffe")]
        // [SerializeField]public float dashValue = 0.2f;

        [SerializeField]
        private Text txtScore;
        [SerializeField]
        private Text txtDistancia;
        [SerializeField]
        private Text txtLikes;
        [SerializeField]
        private GameObject[] hidenPlay;

        PlayerController playerController;

        public static GameManager inst;

        public Action ON_START_GAME;
        public Action ON_END_GAME;

        #region GETTER_SETTERS
        public bool isGameRunning{
            get => _isGameRunning;

            set 
            {
                if(value)
                    _isGameRunning = value;
                else
                {
                    _isGameRunning = value;

                    if(ON_END_GAME != null) ON_END_GAME();
                }
            }
        }
        public bool isGamePaused{
            get => _isGamePaused;

            set 
            {
                _isGamePaused = value;
            }
        }
        public bool death { 
            get => _death;

            set {
                if(value)
                {
                    //seta quais itens do menu ir� aparecer ao morrer e verifica se h� um novo high score, se sim, seta na ui
                    if (PlayerPrefs.HasKey("HScore"))
                    {
                        if (PlayerPrefs.GetInt("HScore") < score)
                        {
                            PlayerPrefs.SetInt("HScore", score);
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("HScore", score);
                    }
                    VerificaNovoHighScore();

                    _death = value;
                    isGameRunning = false;

                    hidenPlay[1].SetActive(true);
                    hidenPlay[1].GetComponent<Text>().text = "Game Over";
                    hidenPlay[2].SetActive(true);
                    hidenPlay[3].SetActive(true);
                    hidenPlay[4].SetActive(false);
                }
                else
                {
                    _death = value;
                }
            }
        }
        public float likes 
        {
            get => _likes;

            set 
            {
                _likes = value;
                txtLikes.text = String.Concat("Likes: ", _likes);
            }
        }
        private float travelledDinstance{ 
            get => _travelledDinstance;
            set{
                _travelledDinstance = value;
                txtDistancia.text = String.Concat("Distancia Percorrida: ", MathF.Round(_travelledDinstance));
            } 
        }

        public float currentDificult{
            get => _currentDificult;
        }
        #endregion

        void Awake()
        {
            Screen.SetResolution(1280, 720, false);
            
            if (inst == null)
            {
                inst = this;
            }
            else if (inst != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _currentDificult = initialDificult;
            VerificaNovoHighScore();
            playerController = FindObjectOfType<PlayerController>();
            obstaclesSpeed = initialObstaclesSpeed;
        }

        void Update()
        {
            if(!isGameRunning)
                return;
            travelledDinstance += Time.deltaTime * travelledDinstanceMultiplier * _currentDificult;//distancia percorrida pelo jogador
            
            if(isInFanInteraction)
                obstaclesSpeed = 0f;
            else
                obstaclesSpeed = initialObstaclesSpeed * currentDificult;

            UpdateDificult();

            
        }

        void UpdateDificult()
        {
            _currentDificult += Time.deltaTime * (difcicultMultiplier/100);

            if(!isInFanInteraction)
                playerController.animator.SetFloat("velx", 1 * _currentDificult);
            else
                playerController.animator.SetFloat("velx", (1 * _currentDificult)*0.2f);

        }

        private void VerificaNovoHighScore() {
            if (PlayerPrefs.HasKey("HScore"))
            {
                hidenPlay[2].GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("HScore");
            }
            else {
                hidenPlay[2].GetComponent<Text>().text = "High Score: 0";
            }

        }
        public void addScore() {
            score++;
            txtScore.text = "Score: " + score;
        }

        public void StartGame() {
            //seta quais itens do menu ir� aparecer ao iniciar e init = true
            hidenPlay[0].SetActive(false);
            hidenPlay[1].SetActive(false);
            hidenPlay[2].SetActive(false);
            hidenPlay[4].SetActive(true);
            _isGameRunning = true;
            if(ON_START_GAME != null) ON_START_GAME();
        }

        public void loadScene() {
            //recarrega a cena(play again)
            hidenPlay[3].GetComponent<Button>().interactable = false;
            SceneManager.LoadScene("Main");
        }

        public void DashPlayer(float dashValue)
        {
            playerController.playerXPositionPercentage += Mathf.Lerp(0, dashValue, 1f);
        }

        // public bool getDeath()
        // {
        //     return _death;
        // }
        // public void SetDeath()
        // {
        //     //seta quais itens do menu ir� aparecer ao morrer e verifica se h� um novo high score, se sim, seta na ui
        //     if (PlayerPrefs.HasKey("HScore"))
        //     {
        //         if (PlayerPrefs.GetInt("HScore") < score)
        //         {
        //             PlayerPrefs.SetInt("HScore", score);
        //         }
        //     }
        //     else
        //     {
        //         PlayerPrefs.SetInt("HScore", score);
        //     }
        //    VerificaNovoHighScore();
        //    _death = true;
        //    hidenPlay[1].SetActive(true);
        //    hidenPlay[1].GetComponent<Text>().text = "Game Over";
        //    hidenPlay[2].SetActive(true);
        //    hidenPlay[3].SetActive(true);
        //    hidenPlay[4].SetActive(false);
        // }

        // public bool getInit()
        // {
        //     return _isGameRunning;
        // }
        // public void SetInit(bool p)
        // {
        //     _isGameRunning = p;
        // }
    }
}
