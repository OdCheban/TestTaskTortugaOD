using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gameDream
{
    public class PlayerStats : MonoBehaviour
    {
        public bool shield;
        GameObject shieldObj;

        GameObject spaceObj;
        float gravityNow;

        void Start()
        {
            shieldObj = transform.Find("Shield").gameObject;
            spaceObj = transform.Find("Space").gameObject;
            spaceObj.SetActive(false);
            shieldObj.SetActive(false);
        }

        public void GetShield()
        {
            shield = true;
            shieldObj.SetActive(true);
        }
        public void GetSpace(int jumpK)
        {
            gameObject.tag = "Astronaut";
            spaceObj.SetActive(true);
            gravityNow = GetComponent<EnginePlayer>().gravity;
            GetComponent<EnginePlayer>().gravity = 20;
            GetComponent<EnginePlayer>().KJumpSpace += jumpK;
        }

        public void DestroySpace()
        {
            gameObject.tag = "Player";
            spaceObj.SetActive(false);
            GetComponent<EnginePlayer>().gravity = gravityNow;
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
            GetComponent<EnginePlayer>().enabled = false;
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