using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class SpaceBonus : Bonus
    {
        [SerializeField]
        int jumpKSpace;
        protected override void BonusAdd(Transform other)
        {
            other.GetComponent<PlayerStats>().GetSpace(jumpKSpace);
        }
    }
}