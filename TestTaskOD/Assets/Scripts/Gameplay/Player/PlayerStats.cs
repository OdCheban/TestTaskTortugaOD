using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gameDream
{
    public class PlayerStats : MonoBehaviour
    {
        struct PlayerStatsStart
        {
            public float gravity,forceJump;
            public PlayerStatsStart(float g,float f)
            {
                gravity = g;
                forceJump = f;
            }
        }

        public bool shield;
        GameObject shieldObj;

        EnginePlayer ep;
        GameObject spaceObj;
        PlayerStatsStart statsStart;

        public AudioClip giveBonusSound;
        AudioSource audioSource;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            shieldObj = transform.Find("Shield").gameObject;
            spaceObj = transform.Find("Space").gameObject;
            spaceObj.SetActive(false);
            shieldObj.SetActive(false);

            ep = GetComponent<EnginePlayer>();
            statsStart = new PlayerStatsStart(ep.gravity, ep.forceJump);
        }

        public void GiveBonusSound()
        {
            audioSource.PlayOneShot(giveBonusSound);
        }

        public void GetShield()
        {
            shield = true;
            shieldObj.SetActive(true);
        }
        public void GetSpace(int jumpK)
        {
            spaceObj.SetActive(true);
            ep.gravity = 25;
            ep.forceJump = 15;
            ep.KJumpSpace += jumpK;
        }

        public void DestroySpace()
        {
            spaceObj.SetActive(false);
            ep.gravity = statsStart.gravity;
            ep.forceJump = statsStart.forceJump;
        }
        public void DestroyShield()
        {
            shield = false;
            shieldObj.SetActive(false);
        }
        
        void OffRender()
        {
            GetComponent<MeshRenderer>().enabled = false;
            shieldObj.SetActive(false);
            spaceObj.SetActive(false);
        }
        public void Kill()
        {
            ep.enabled = false;
            GetComponent<CharacterController>().enabled = false;

            GameObject particleDeath = (GameObject)Instantiate(Resources.Load(AllFunc.GetPathParticleDeath())) as GameObject;
            particleDeath.transform.position = transform.position;
            OffRender();
            Invoke("DefeatGame", 1.5f);
        }
        void DefeatGame()
        {
            AllFunc.Defeat();
        }
    }
}