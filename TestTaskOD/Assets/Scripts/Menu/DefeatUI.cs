using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
#endif
namespace gameDream
{
    public class DefeatUI : MonoBehaviour {
        public Text pointsText;
        public Text recordText;

        #if UNITY_ANDROID
        private const string achiv2 = "CgkIyLf06YcTEAIQAg";//10 очков
        private const string achiv3 = "CgkIyLf06YcTEAIQAw";//смерть
        private const string leaderBoard = "CgkIyLf06YcTEAIQBA";
        #endif

        void FinishUI(int point)
        {
            transform.root.Find("PointsText").GetComponent<Text>().enabled = false;
            pointsText.text = point + "!";
            transform.GetChild(0).gameObject.SetActive(true);
            transform.Find("Panel/TopPanel").GetComponent<EasyTween>().enabled = true;
            recordText.text = "Рекорд: " + PlayerPrefs.GetInt("score");
        }

        void RefreshStats(int point)
        {
            if (point > PlayerPrefs.GetInt("score")) //или же синглтон в объекте(донтдестройонлоад), тогда не придется каждый раз обращаться к памяти 
                PlayerPrefs.SetInt("score", point);

            #if UNITY_ANDROID
            Social.ReportScore(point, leaderBoard, (bool success) =>{
                if (success) Debug.Log("new record");
            });
            AllFunc.GetAchive(achiv3);
            if(point >= 10)
                AllFunc.GetAchive(achiv2);
#           endif
        }

        public void ActivateDefeate()
        {
            int point = GameObject.Find("GamePlay").GetComponent<ManagementGame>().GamePoints;
            RefreshStats(point);
            FinishUI(point);
        }
        public void Restart()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("scGame");
        }
        #if UNITY_ANDROID
        public void AchiveGame()
        {
            Social.ShowAchievementsUI();
        }
        public void LeaderBoardGame()
        {
            Social.ShowLeaderboardUI();
        }
        #endif
    }
}