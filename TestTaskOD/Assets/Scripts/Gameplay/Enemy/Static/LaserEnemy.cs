using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class LaserEnemy : StaticEnemy
    {
        [SerializeField]
        float interval;
        bool on;

        protected override GameObject GetItemKill()
        {
            GameObject laser = transform.Find("Laser").gameObject;
            laser.SetActive(false);
            return laser;
        }

        public override void DeactivateItemKill()
        {
            on = false;
            itemKill.SetActive(on);
            StopCoroutine("LaserOn");
        }
        public override void ActivateItemKill()
        {
            StartCoroutine("LaserOn");
        }

        private IEnumerator LaserOn()
        {
            while (true)
            {
                on = !on;
                itemKill.SetActive(on);
                yield return new WaitForSeconds(interval);
            }
        }
    }
}