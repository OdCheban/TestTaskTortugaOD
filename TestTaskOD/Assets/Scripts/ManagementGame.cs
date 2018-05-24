using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagementGame : MonoBehaviour {
    PlatformBlock[] platform;
    int kBlock;

    Text textPoints;
    private int _gamePoints;
    public int GamePoints
    {
        get { return _gamePoints; }
        set {
            _gamePoints = value;
            textPoints.text = _gamePoints.ToString();
        }
    }
    private void Start()
    {
        textPoints = GameObject.Find("Canvas/PointsText").GetComponent<Text>();
        platform = transform.Find("Platform").GetComponentsInChildren<PlatformBlock>();
        kBlock = platform.Length;
    }

    public void NextPlatform()
    {
        platform[GamePoints % kBlock].transform.position = new Vector3(0, kBlock + GamePoints, kBlock + GamePoints);
        GamePoints++;
    }
}
