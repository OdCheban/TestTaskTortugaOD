using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class TimeBonus : Bonus
    {
        ManagementGame mg;
        [SerializeField]
        float timeStopEnemy;

        protected override void BonusAdd(Transform other)
        {
            mg = GameObject.Find("GamePlay").GetComponent<ManagementGame>();
            mg.StopEnemy(timeStopEnemy);
        }
    }
}