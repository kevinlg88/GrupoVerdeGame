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
        private bool _death = false;
        public bool isInFanInteraction = false;

        private float _travelledDinstance;
        [SerializeField]
        private float travelledDinstanceMultiplier = 1f;
        private float _likes;

        public float slowPercentage = 1f;

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
            VerificaNovoHighScore();
            playerController = FindObjectOfType<PlayerController>();
        }

        void Update()
        {
            if(!isGameRunning)
                return;

            travelledDinstance += Time.deltaTime * travelledDinstanceMultiplier;
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

        public void SlowEverything()//para quando esta na interaacao com o fan
        {
            
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
