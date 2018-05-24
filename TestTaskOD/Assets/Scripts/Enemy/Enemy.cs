using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        float radiusEnemy;
        Vector3 moveDir = Vector3.zero;
        [SerializeField]
        protected float jumpSpeed;
        [SerializeField]
        float gravity;

        private void Start()
        {
            Destroy(gameObject, 10.0f);
        }

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
        private void Update()
        {
            MoveEnemy();
            RotateEnemy();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Time.timeScale = 0;
            }
        }
    }
}