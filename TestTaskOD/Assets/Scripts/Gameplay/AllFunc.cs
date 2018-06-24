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
            GameObject.Find("Canvas/DefeatUI").GetComponent<DefeatUI>().ActivateDefeate();
        }
        public static string GetPathParticleDeath()
        {
            return "Particle/deathParticle";
        }
    }
}
