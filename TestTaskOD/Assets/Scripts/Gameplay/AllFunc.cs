using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gameDream
{
    public class AllFunc : MonoBehaviour
    {
        public static float spawnXmin = -4.5f;
        public static float spawnXmax = 4.5f;
        public static float scaleBlock = 1.25f;
        #if UNITY_ANDROID
        public static void GetAchive(string id)
        {
            Social.ReportProgress(id, 100, (bool success) => {
                if (success) Debug.Log("nice");
            });
        }
        #endif

        public static void Defeat()
        {
           // Time.timeScale = 0;
            GameObject.Find("Canvas/DefeatUI").GetComponent<DefeatUI>().ActivateDefeate();
        }
        public static string[] GetPathEnemyDynamic()
        {
            string[] s = {
                "EnemyDynamic\\CubeEnemy",
                "EnemyDynamic\\EnemyBigSphere",
                "EnemyDynamic\\EnemyCylinder",
                "EnemyDynamic\\EnemySmallSphere"
            };
            return  s;
        }
        public static string[] GetPathEnemyStatic()
        {
            string[] s = {
                "EnemyStatic\\LaserEnemy",
                "EnemyStatic\\Saw"
            };
            return s;
        }
        public static string[] GetPathBonus()
        {
            string[] s = {
                "Bonus\\SheildBonus",
                "Bonus\\SpaceBonus",
                "Bonus\\TimerBonus"
            };
            return s;
        }
        public static string GetPathParticleDeath()
        {
            return "Particle/deathParticle";
        }
    }
}
