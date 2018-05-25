using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        float intervalJump;

        int _kJumpSpace;
        public int KJumpSpace
        {
            get { return _kJumpSpace; }
            set {
                _kJumpSpace = value;
            }
        }
        bool destroySpaceAfterJump;
        bool jump;

        private void Start()
        {
            GameControl = GameObject.Find("GamePlay").GetComponent<ManagementGame>();
            controller = GetComponent<CharacterController>();
            radiusPlayer = GetComponent<CharacterController>().radius + 0.1f;
        }

        bool IsGround()
        {
            return (Physics.Raycast(transform.position, Vector3.forward, radiusPlayer) && Physics.Raycast(transform.position, -Vector3.up, radiusPlayer)) ? true : false;
        }

        void IfSpace()
        {
            if (KJumpSpace > 0)
            {
                GameControl.NextPlatform();
                KJumpSpace--;
                destroySpaceAfterJump = (KJumpSpace == 0) ? true : false;
            }
        }

        private void Update()
        {
            intervalJump += Time.deltaTime;
            #if UNITY_EDITOR
            Action("Idle");
            if (Input.GetKeyDown(KeyCode.Space))
                Action("Forward");
            if (Input.GetKeyDown(KeyCode.A))
                Action("Left");
            if (Input.GetKeyDown(KeyCode.D))
                Action("Right");
            #endif

            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }

        Vector3 GetForceJumpForward()
        {
            float z = (intervalJump < 0.5f) ? 5/intervalJump : 3; // 5 - test...
            intervalJump = 0.0f;
            return new Vector3(0, jumpSpeed, z);
        }

        public void Action(string Dir)
        {
            if (IsGround())
            {
                switch (Dir)
                {
                    case "Forward":
                        IfSpace();
                        GameControl.NextPlatform();
                        moveDir = GetForceJumpForward();
                        break;
                    case "Left":
                        moveDir = new Vector3(-3, jumpSpeed, 0);
                        break;
                    case "Right":
                        moveDir = new Vector3(3, jumpSpeed, 0);
                        break;
                    case "Idle":
                        moveDir = Vector3.zero;
                        break;
                }

                if (destroySpaceAfterJump)
                {
                    GetComponent<PlayerStats>().DestroySpace();
                    destroySpaceAfterJump = false;
                }
            }
        }
    }
}