using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class StaticEnemy : MonoBehaviour, TimeControl
    {
        protected GameObject itemKill;
        protected virtual GameObject GetItemKill() { return null; }
        protected virtual void DeactivateItemKill() {}
        protected virtual void ActivateItemKill() { }
        protected float timeStop = -1;
        public void Activate()
        {
            itemKill = GetItemKill();
            ActivateItemKill();
        }
        
        void Update()
        {
            if (timeStop > 0)
                timeStop -= Time.deltaTime;
            else if (timeStop <= 0 && timeStop > -1)
            {
                СontinueEnemy();
                timeStop = -1;
            }
        }
        public void StopEnemy(float sec)
        {
            timeStop = sec;
            DeactivateItemKill();
        }

        public void СontinueEnemy()
        {
            ActivateItemKill();
        }
    }
}