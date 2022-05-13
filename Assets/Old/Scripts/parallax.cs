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
            if(GameManager.inst.slowPercentage == 1f)
                vel = initialVel;
            else
                vel *= 0.9f;
            //caso o player esteja se movendo, aplica o efeito de parallax no background1
            if (GameManager.inst.isGameRunning) {
                mshr.material.mainTextureOffset += new Vector2(vel * Time.deltaTime, 0);
            }
        }
    }
}
