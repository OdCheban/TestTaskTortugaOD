using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class FallPlayer : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            other.GetComponent<PlayerStats>().Kill();
        }
    }
}