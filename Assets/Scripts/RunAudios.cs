using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenTeam
{
    public class RunAudios : MonoBehaviour
    {
        public void playRun1()
        {
            GameManager.inst.audioManager.run1.Play();
        }

        public void playRun2()
        {
            GameManager.inst.audioManager.run2.Play();
        }
        public void playButton()
        {
            GameManager.inst.audioManager.button.Play();
        }
    }
}
