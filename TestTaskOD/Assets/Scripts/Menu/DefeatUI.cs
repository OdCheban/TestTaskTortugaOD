using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace gameDream
{
    public class DefeatUI : MonoBehaviour {
        public Text pointsText;
        public Text recordText;

        public void ActivateDefeate()
        {
            transform.root.Find("PointsText").GetComponent<Text>().enabled = false;
            pointsText.text = GameObject.Find("GamePlay").GetComponent<ManagementGame>().GamePoints + "!";
            recordText.text = "Рекорд: 0";
            transform.GetChild(0).gameObject.SetActive(true);
            transform.Find("Panel/TopPanel").GetComponent<EasyTween>().enabled = true;
        }
        public void Restart()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("scGame");
        }
    }
}