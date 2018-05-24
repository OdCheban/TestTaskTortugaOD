using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gameDream
{
    public class CubeEnemy : DynamicEnemy
    {
        bool side;
        public float speedRotate;
        protected override Vector3 GetDir()
        {
            side = !side;
            if(side)
                return new Vector3(2, jumpSpeed, -1.0f);
            else
                return new Vector3(-2, jumpSpeed, -1.0f);
        }
        protected override void RotateEnemy()
        {
            if(side)
                transform.Rotate(new Vector3(1, 1, 1), speedRotate * Time.deltaTime);
            else
                transform.Rotate(new Vector3(1,-1,-1), speedRotate * Time.deltaTime);
        }
    }
}
