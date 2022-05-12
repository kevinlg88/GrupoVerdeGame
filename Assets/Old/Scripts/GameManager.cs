using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int score = 0;
    [SerializeField]
    public bool isGameRunning = false;
    private bool _death = false;

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

    [SerializeField]
    private Text txtScore;
    [SerializeField]
    private GameObject[] hidenPlay;
    public static GameManager inst;

    public Action ON_START_GAME;
    public Action ON_END_GAME; //FALTA IMPLEMENTAR

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
        isGameRunning = true;
        if(ON_START_GAME != null) ON_START_GAME();
    }

    public void loadScene() {
        //recarrega a cena(play again)
        hidenPlay[3].GetComponent<Button>().interactable = false;
        SceneManager.LoadScene("Main");
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
    //     return isGameRunning;
    // }
    // public void SetInit(bool p)
    // {
    //     isGameRunning = p;
    // }
}
