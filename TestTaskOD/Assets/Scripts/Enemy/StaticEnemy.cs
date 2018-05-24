using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class StaticEnemy : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Time.timeScale = 0;
            }
        }
    }
}
