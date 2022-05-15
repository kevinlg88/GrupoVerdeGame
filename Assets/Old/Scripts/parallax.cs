using System.Timers;
using UnityEngine;

namespace GreenTeam
{
    [RequireComponent(typeof(MeshRenderer))]
    public class parallax : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer mshr;
        [SerializeField]
        private float vel;
        private float initialVel;
        [SerializeField]
        private Animator animator;

        void Start()
        {
            mshr = gameObject.GetComponent<MeshRenderer>();
            initialVel = vel;

        }
        
        void Update()
        {
            
            if (GameManager.inst.isGameRunning) {
                if(GameManager.inst.isInFanInteraction)
                    mshr.material.mainTextureOffset += new Vector2((vel*0.2f) * Time.deltaTime, 0);
                else
                    mshr.material.mainTextureOffset += new Vector2((vel*GameManager.inst.currentDificult) * Time.deltaTime, 0);
            }
        }
    }
}
