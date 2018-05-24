using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace gameDream
{
    public class CylinderEnemy : Enemy
    {
        public float speedRotate;
        protected override void RotateEnemy()
        {
            transform.Rotate(Vector3.right, speedRotate * Time.deltaTime);
        }
    }
}
