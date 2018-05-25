using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public AudioClip jumpSound;
        AudioSource audioSource;

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
            audioSource = GetComponent<AudioSource>();
            GameControl = GameObject.Find("GamePlay").GetComponent<ManagementGame>();
            controller = GetComponent<CharacterController>();
            radiusPlayer = GetComponent<CharacterController>().radius + 0.1f;
        }

        bool IsGround()
        {
            return (Physics.Raycast(transform.position, Vector3.forward, radiusPlayer) && Physics.Raycast(transform.position, -Vector3.up, radiusPlayer)) ? true : false;
        }

        void IfSpaceBonus()
        {
            if (KJumpSpace > 0)
            {
                GameControl.NextPlatform();//с бонусом прыгаем сразу на 3 платформы вперед, здесь +2
                GameControl.NextPlatform();
                KJumpSpace--;
                destroySpaceAfterJump = (KJumpSpace <= 0) ? true : false;
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("scMenu");
            }
            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }

        Vector3 GetForceJumpForward()
        {
            float z = (intervalJump < 0.5f) ? 4/intervalJump : 4; // 4 - test...
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
                        IfSpaceBonus();
                        GameControl.NextPlatform();
                        moveDir = GetForceJumpForward();
                        audioSource.PlayOneShot(jumpSound);
                        break;
                    case "Left":
                        moveDir = new Vector3(-3, jumpSpeed, 0);
                        audioSource.PlayOneShot(jumpSound);
                        break;
                    case "Right":
                        moveDir = new Vector3(3, jumpSpeed, 0);
                        audioSource.PlayOneShot(jumpSound);
                        break;
                    case "Idle":
                        moveDir = Vector3.zero;
                        if (destroySpaceAfterJump)
                        {
                            GetComponent<PlayerStats>().DestroySpace();
                            destroySpaceAfterJump = false;
                        }
                        break;
                }
            }else if(Dir == "Forward")//пока игрок в полете игрок вдруг решил много много раз тапать
            {
                moveDir.z *= 2;
            }
        }
    }
}