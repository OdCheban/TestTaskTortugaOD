using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class DynamicEnemy : MonoBehaviour
    {
        [SerializeField]
        float radiusEnemy;
        protected Vector3 moveDir = Vector3.zero;
        [SerializeField]
        protected float jumpSpeed;
        [SerializeField]
        float gravity;
        public float timerLive;

        bool IsGround()
        {
            return (Physics.Raycast(transform.position, -Vector3.up, radiusEnemy)) ? true : false;
        }

        void JumpEnemy()
        {
            moveDir = GetDir();
        }
        protected virtual Vector3 GetDir()
        {
            return new Vector3(0, jumpSpeed, -1.0f);
        }
        protected virtual void RotateEnemy()
        {}

        private void MoveEnemy()
        {
            if (IsGround())
            {
                JumpEnemy();
            }
            else
            {
                moveDir.y -= gravity * Time.deltaTime;
            }
            transform.position += moveDir * Time.deltaTime;
        }

        protected virtual void CheckExitBound()
        {
        }


        public bool Action()
        {
            CheckExitBound();
            MoveEnemy();
            RotateEnemy();

            timerLive -= Time.deltaTime;
            return (timerLive > 0);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (other.GetComponent<PlayerStats>().shield)
                {
                    other.GetComponent<PlayerStats>().DestroyShield();
                    timerLive = 0;
                    //Destroy(gameObject);
                }
                else
                {
                    other.GetComponent<PlayerStats>().Kill();
                }
            }
        }
    }
}