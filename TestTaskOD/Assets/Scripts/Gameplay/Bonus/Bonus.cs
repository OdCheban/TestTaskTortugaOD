using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class Bonus : MonoBehaviour
    {
        [SerializeField]
        float speedRotate;

        protected virtual void BonusAdd(Transform other)
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" || other.tag == "Astronaut")
            {
                other.GetComponent<PlayerStats>().GiveBonusSound();
                BonusAdd(other.transform);
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            transform.Rotate(new Vector3(0, 1, 0), speedRotate * Time.deltaTime);
        }
    }
}