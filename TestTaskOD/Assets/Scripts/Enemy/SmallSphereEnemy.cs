using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gameDream
{
    public class SmallSphereEnemy : Enemy
    {
        protected override float GetRadius()
        {
            return GetComponent<SphereCollider>().radius + 0.1f;
        }
    }
}