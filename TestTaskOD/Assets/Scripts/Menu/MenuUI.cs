using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour {
    public Sprite spriteSoundOn;
    public Sprite spriteSoundOff;
    Image imageSound;

    void Start()
    {
        imageSound = transform.Find("BtnSound").GetComponent<Image>();
    }
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
