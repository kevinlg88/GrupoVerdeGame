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

        [Tooltip("Velocida do qão lento fica o paralax durante a interação com o fan. 0.2 = 20%")]
        [SerializeField] private float slowSpeed = 0.2f;

        void Start()
        {
            mshr = gameObject.GetComponent<MeshRenderer>();
        }
        
        void Update()
        {
            
            if (GameManager.inst.isGameRunning) {
                if(GameManager.inst.isInFanInteraction)
                    mshr.material.mainTextureOffset += new Vector2((vel*slowSpeed) * Time.deltaTime, 0);
                else
                    mshr.material.mainTextureOffset += new Vector2((vel*GameManager.inst.currentDificult) * Time.deltaTime, 0);
            }
        }
    }
}
