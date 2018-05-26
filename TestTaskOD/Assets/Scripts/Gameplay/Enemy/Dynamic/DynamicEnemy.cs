using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class DynamicEnemy : MonoBehaviour, TimeControl
    {
        [SerializeField]
        float radiusEnemy;
        protected Vector3 moveDir = Vector3.zero;
        [SerializeField]
        protected float jumpSpeed;
        [SerializeField]
        float gravity;
        float timerLive;
        bool stop;
        
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
        {
            
        }
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
        private void OldAge()
        {
            timerLive += Time.deltaTime;
            if (timerLive > 20.0f)
                Destroy(gameObject);
        }

        protected virtual void CheckExitBound()
        {
        }
        
        private void Update()
        {
            if (!stop)
            {
                CheckExitBound();
                OldAge();
                MoveEnemy();
                RotateEnemy();
            }
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (other.GetComponent<PlayerStats>().shield)
                {
                    other.GetComponent<PlayerStats>().DestroyShield();
                    Destroy(gameObject);
                }
                else
                {
                    other.GetComponent<PlayerStats>().Kill();
                }
            }
        }

        public void StopEnemy(float sec)
        {
            stop = true;
            Invoke("СontinueEnemy", sec);
        }

        public void СontinueEnemy()
        {
            stop = false;
        }
    }
}