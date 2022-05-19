using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenTeam
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource run1;
        public AudioSource run2;
        public AudioSource slide;
        public AudioSource jump;
        public AudioSource death;
        public AudioSource musicMenu;
        public AudioSource musicRunnings;
        public AudioSource powerUp;
        public AudioSource button;
        public AudioSource buttonBack;
        public AudioSource dash;
        public AudioSource fan;
        public AudioSource obstacleCollision;
        public AudioSource photographer;
        public AudioSource like;

        
        public void playButton()
        {
            button.Play();
        }
    }
}
