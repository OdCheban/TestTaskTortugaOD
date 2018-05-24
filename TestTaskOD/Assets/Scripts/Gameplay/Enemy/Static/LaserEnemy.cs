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
        static IEnumerator coroutine;

        protected override GameObject GetItemKill()
        {
            coroutine = LaserOn();
            GameObject laser = transform.Find("Laser").gameObject;
            laser.SetActive(false);
            return laser;
        }

        protected override void DeactivateItemKill()
        {
            on = false;
            itemKill.SetActive(on);
            StopCoroutine(coroutine);
        }
        protected override void ActivateItemKill()
        {
            coroutine = LaserOn();
            StartCoroutine(coroutine);
        }

        private IEnumerator LaserOn()
        {
            while (true)
            {
                if (timeStop < 0)
                {
                    on = !on;
                    itemKill.SetActive(on);
                }
                else
                {
                    yield return null;
                }
                yield return new WaitForSeconds(interval);
            }
        }
    }
}