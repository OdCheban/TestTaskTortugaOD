using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class StaticEnemy : MonoBehaviour
    {
        protected GameObject itemKill;
        protected virtual GameObject GetItemKill() { return null; }
        public virtual void DeactivateItemKill() {}
        public virtual void ActivateItemKill() { }
        public void Activate()
        {
            itemKill = GetItemKill();
            ActivateItemKill();
        }
        

    }
}