using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class parallax : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer mshr;
    [SerializeField]
    private float vel;
    [SerializeField]
    private Animator animator;

    void Start()
    {
        mshr = gameObject.GetComponent<MeshRenderer>();
    }
    
    void Update()
    {
        //caso o player esteja se movendo, aplica o efeito de parallax no background1
        if (animator.GetFloat("velx") == 10f) {
            mshr.material.mainTextureOffset += new Vector2(vel * Time.deltaTime, 0);
        }
    }
}
