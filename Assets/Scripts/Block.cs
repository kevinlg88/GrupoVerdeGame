using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreenTeam
{
    public class Block : MonoBehaviour
    {
        void Update()
        {
            int count = gameObject.GetComponentsInChildren<Transform>().Count();
            if(count <= 2)
                Destroy(gameObject);
        }
    }
}
