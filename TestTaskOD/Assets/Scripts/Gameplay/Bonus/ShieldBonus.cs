using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class ShieldBonus : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerStats>().GetShield();
                Destroy(gameObject);
            }
        }
    }
}