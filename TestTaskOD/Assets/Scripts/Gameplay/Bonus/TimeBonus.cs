using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class TimeBonus : MonoBehaviour
    {
        ManagementGame mg;
        [SerializeField]
        float timeStopEnemy;
        [SerializeField]
        float speedRotate;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                mg = GameObject.Find("GamePlay").GetComponent<ManagementGame>();
                mg.StopEnemy(timeStopEnemy);
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            transform.Rotate(new Vector3(0, 1, 0), speedRotate * Time.deltaTime);
        }
    }
}