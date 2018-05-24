using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gameDream
{
    public class PlayerStats : MonoBehaviour
    {
        public bool shield;
        GameObject shieldObj;
        
        void Start()
        {
            shieldObj = transform.Find("Shield").gameObject;
            shieldObj.SetActive(false);
        }

        public void GetShield()
        {
            shield = true;
            shieldObj.SetActive(true);
        }
        public void DestroyShield()
        {
            shield = false;
            shieldObj.SetActive(false);
        }
    }
}