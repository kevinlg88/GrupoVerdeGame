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
        public bool isInSineEffect = false;

        [Tooltip("Dificuldade inicial do jogo. 1f = 100%")]
        [SerializeField] float initialDificult = 1f;

        [Tooltip("Dificuldade maxima do jogo. 1f = 200%")]
        [SerializeField] float maxDificult = 2f;
        float _currentDificult;

        [Tooltip("Define o quão rapido o a dificuldade vai aumentar")]
        [SerializeField] float difcicultMultiplier = 1f;
        private float _travelledDinstance;

        private float travelledDinstanceMultiplier = 1f;
        private int _likes;
        private int totalLikes;

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

        [SerializeField] GameObject MenuCanvas;
        [SerializeField] GameObject PauseCanvas;
        [SerializeField] GameObject RunningCanvas;
        public GameObject x2Likes;
        [SerializeField] GameObject GameOverCanvas;
        [SerializeField] GameObject ShopCanvas;

        public AudioManager audioManager;
        PlayerController playerController;

        public static GameManager inst;

        public Action ON_START_GAME;
        public Action ON_END_GAME;

        #region GETTER_SETTERS
        public bool isGameRunning
        {
            get => _isGameRunning;

            set
            {
                if (value)
                    _isGameRunning = value;
                else
                {
                    _isGameRunning = value;

                    if (ON_END_GAME != null) ON_END_GAME();
                }
            }
        }
        public bool isGamePaused
        {
            get => _isGamePaused;

            set
            {
                _isGamePaused = value;
            }
        }
        public bool death
        {
            get => _death;

            set
            {
                if (value)
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

                    if (PlayerPrefs.HasKey("Likes"))
                    {
                        totalLikes = PlayerPrefs.GetInt("Likes");
                        totalLikes += _likes;
                        PlayerPrefs.SetInt("Likes", totalLikes);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Likes", _likes);
                    }

                    _death = value;
                    isGameRunning = false;

                    RunningCanvas.SetActive(false);
                    GameOverCanvas.SetActive(true);
                }
                else
                {
                    _death = value;
                }
            }
        }
        public int likes
        {
            get => _likes;

            set
            {
                _likes = value;
                // txtLikes.text = String.Concat("Likes: ", _likes);
                txtLikes.text = _likes.ToString();
            }
        }
        private float travelledDinstance
        {
            get => _travelledDinstance;
            set
            {
                _travelledDinstance = value;
                txtDistancia.text = String.Concat("Distancia Percorrida: ", MathF.Round(_travelledDinstance));
            }
        }

        public float currentDificult
        {
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
            if (PlayerPrefs.HasKey("Likes"))
            {
                totalLikes = PlayerPrefs.GetInt("Likes");
                PlayerPrefs.SetInt("Likes", totalLikes);
            }
            else
            {
                PlayerPrefs.SetInt("Likes", _likes);
            }


            // txtLikes.text = String.Concat("Likes :", totalLikes);
            txtLikes.text = totalLikes.ToString();

            _currentDificult = initialDificult;
            VerificaNovoHighScore();
            playerController = FindObjectOfType<PlayerController>();
            obstaclesSpeed = initialObstaclesSpeed;
        }

        void Update()
        {
            if (!isGameRunning)
                return;
            travelledDinstance += Time.deltaTime * travelledDinstanceMultiplier * _currentDificult;//distancia percorrida pelo jogador

            if (isInFanInteraction)
                obstaclesSpeed = 0f;
            else
                obstaclesSpeed = initialObstaclesSpeed * currentDificult;

            UpdateDificult();


        }

        void UpdateDificult()
        {
            if (_currentDificult < maxDificult)
                _currentDificult += Time.deltaTime * (difcicultMultiplier / 100);

            if (!isInFanInteraction)
                playerController.animator.SetFloat("velx", 1 * _currentDificult);
            else
                playerController.animator.SetFloat("velx", (1 * _currentDificult) * 0.2f);

        }

        private void VerificaNovoHighScore()
        {
            if (PlayerPrefs.HasKey("HScore"))
            {
                hidenPlay[2].GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("HScore");
            }
            else
            {
                hidenPlay[2].GetComponent<Text>().text = "High Score: 0";
            }

        }
        public void addScore()
        {
            score++;
            txtScore.text = "Score: " + score;
        }

        public void StartGame()
        {

            MenuCanvas.SetActive(false);
            GameOverCanvas.SetActive(false);
            RunningCanvas.SetActive(true);
            // txtLikes.text = String.Concat("Likes: ",_likes);
            txtLikes.text = _likes.ToString();

            GameManager.inst.audioManager.musicMenu.Stop();
            GameManager.inst.audioManager.musicRunnings.Stop();

            _isGameRunning = true;
            if (ON_START_GAME != null) ON_START_GAME();
        }

        public void loadScene()
        {
            //recarrega a cena(play again)
            // hidenPlay[3].GetComponent<Button>().interactable = false;
            SceneManager.LoadScene("Main");
        }

        public void ChangeXPlayer(float dashValue, float duration = 1f)
        {
            playerController.playerXPositionPercentage += dashValue;
        }

    }
}
