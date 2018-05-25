using System.Collections;
using System.Collections.Generic;
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
    public class MenuUI : MonoBehaviour
    {
        public Sprite spriteSoundOn;
        public Sprite spriteSoundOff;
        Image imageSound;

        private const string achiv1 = "CgkIyLf06YcTEAIQAQ";//первый вход в игру

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                #if UNITY_ANDROID
                PlayGamesPlatform.Instance.SignOut();
                #endif
                Application.Quit();
            }
        }
        void Start()
        {
            imageSound = transform.Find("Panel/BtnSound").GetComponent<Image>();

            #if UNITY_ANDROID
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    //Удачно вошли
                }
                else
                {
                    //Ошибка
                }
            });
            #endif
            if (!PlayerPrefs.HasKey("score"))
                PlayerPrefs.SetInt("score", 0);

            #if UNITY_ANDROID
            AllFunc.GetAchive(achiv1);
            #endif
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
        public void PlayGame()
        {
            SceneManager.LoadScene("scGame");
        }
        public void SoundGame()
        {
            if (AudioListener.volume > 0)
            {
                imageSound.sprite = spriteSoundOff;
                AudioListener.volume = 0;
            }
            else
            {
                imageSound.sprite = spriteSoundOn;
                AudioListener.volume = 1;
            }
        }
        public void LikeGame()
        {

        }
    }
}