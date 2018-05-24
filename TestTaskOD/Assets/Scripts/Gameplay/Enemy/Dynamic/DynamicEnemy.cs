using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class DynamicEnemy : MonoBehaviour, TimeControl
    {
        [SerializeField]
        float radiusEnemy;
        Vector3 moveDir = Vector3.zero;
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
                moveDir = GetDir();
            }
            moveDir.y -= gravity * Time.deltaTime;
            transform.position += moveDir * Time.deltaTime;
        }
        public void OldAge()
        {
            timerLive += Time.deltaTime;
            if (timerLive > 5.0f)
                Destroy(gameObject);
        }
        private void Update()
        {
            if (!stop)
            {
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
                    Time.timeScale = 0;
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