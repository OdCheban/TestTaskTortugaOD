using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gameDream
{
    public class AllFunc : MonoBehaviour
    {
        public static void Defeat()
        {
            Time.timeScale = 0;
            GameObject.Find("Canvas/DefeatUI").GetComponent<DefeatUI>().ActivateDefeate();
        }
    }
}
