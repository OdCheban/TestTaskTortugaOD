﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class ShieldBonus : Bonus
    {
        protected override void BonusAdd(Transform other)
        {
            other.GetComponent<PlayerStats>().GetShield();
        }
    }
}