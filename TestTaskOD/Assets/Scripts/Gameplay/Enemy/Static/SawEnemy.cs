using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class SawEnemy : StaticEnemy
    {
        protected override GameObject GetItemKill()
        {
            return transform.Find("DynamicSaw").gameObject;
        }
        protected override void DeactivateItemKill()
        {
            itemKill.GetComponent<Animator>().enabled = false;
        }
        protected override void ActivateItemKill()
        {
            itemKill.GetComponent<Animator>().enabled = true;
        }
    }
}