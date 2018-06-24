using UnityEngine;

namespace gameDream
{
    public class TimeBonus : Bonus
    {
        ManagementGame poolControl;
        [SerializeField]
        float timeStopEnemy;

        protected override void BonusAdd(Transform other)
        {
            poolControl= GameObject.Find("GamePlay").GetComponent<ManagementGame>();
            poolControl.Stop(timeStopEnemy);
        }
    }
}