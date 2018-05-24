using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class TrigStaticEnemy : MonoBehaviour
    {
        GameObject GetParent()
        {
            GameObject parentObj =  transform.parent.gameObject;
            while (!parentObj.GetComponent<StaticEnemy>())
                parentObj = parentObj.transform.parent.gameObject;
            return parentObj;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (other.GetComponent<PlayerStats>() && other.GetComponent<PlayerStats>().shield)
                {
                    other.GetComponent<PlayerStats>().DestroyShield();
                    Destroy(GetParent());
                }
                else
                {
                    AllFunc.Defeat();
                    Time.timeScale = 0;
                }
            }
        }
    }
}