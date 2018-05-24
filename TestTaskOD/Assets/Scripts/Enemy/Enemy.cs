using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    public class Enemy : MonoBehaviour
    {
        float radiusPlayer;
        Vector3 moveDir = Vector3.zero;
        [SerializeField]
        float jumpSpeed;
        [SerializeField]
        float gravity;

        protected virtual float GetRadius()
        {
            return 0;
        }
        private void Start()
        {
            Destroy(gameObject, 10.0f);
            radiusPlayer = GetRadius();
        }

        bool IsGround()
        {
            if (Physics.Raycast(transform.position, -Vector3.up, radiusPlayer))
                return true;
            else
                return false;
        }

        private void Update()
        {
            if (IsGround())
            {
                moveDir = new Vector3(0, jumpSpeed, -1.0f);
            }
            moveDir.y -= gravity * Time.deltaTime;
            transform.position += moveDir * Time.deltaTime;
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