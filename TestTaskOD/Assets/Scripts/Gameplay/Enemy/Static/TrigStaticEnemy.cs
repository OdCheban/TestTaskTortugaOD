using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class TrigStaticEnemy : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (other.GetComponent<PlayerStats>() && other.GetComponent<PlayerStats>().shield)
                {
                    other.GetComponent<PlayerStats>().DestroyShield();

                    GameObject parentObj = transform.parent.gameObject;
                    while (!parentObj.GetComponent<StaticEnemy>())
                        parentObj = parentObj.transform.parent.gameObject;
                   Destroy(parentObj);
                }
                else
                    Time.timeScale = 0;
            }
        }
    }
}