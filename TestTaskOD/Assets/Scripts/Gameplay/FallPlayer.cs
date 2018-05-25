using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class FallPlayer : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            AllFunc.Defeat();
            Time.timeScale = 0;
        }
    }
}