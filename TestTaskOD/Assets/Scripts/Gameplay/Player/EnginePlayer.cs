using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameDream
{
    [RequireComponent(typeof(CharacterController))]
    public class EnginePlayer : MonoBehaviour
    {
        ManagementGame GameControl;

        public Vector3 moveDir = Vector3.zero;
        public float gravity;
        public float jumpSpeed;
        private float radiusPlayer;
        CharacterController controller;

        private void Start()
        {
            GameControl = GameObject.Find("GamePlay").GetComponent<ManagementGame>();
            controller = GetComponent<CharacterController>();
            radiusPlayer = GetComponent<CharacterController>().radius + 0.1f;
        }

        bool IsGround()
        {
            return (Physics.Raycast(transform.position, -Vector3.up, radiusPlayer)) ? true : false;
        }
        private void Update()
        {
            if (Physics.Raycast(transform.position, Vector3.forward, radiusPlayer) && IsGround())
            {
                moveDir = Vector3.zero;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameControl.NextPlatform();
                    moveDir = new Vector3(0, jumpSpeed, 3);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    moveDir = new Vector3(-3, jumpSpeed, 0);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    moveDir = new Vector3(3, jumpSpeed, 0);
                }
            }
            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }

    }
}