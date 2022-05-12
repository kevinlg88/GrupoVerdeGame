
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField]
    public AudioSource[] sounds;// (0- run1 |1- run 2 |2- slide | 3- jump |4- death |5- score)
    public static PlayerSounds inst;
    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else if (inst != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}
