using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagementGame : MonoBehaviour {
    PlatformBlock[] platform;
    int kBlock;

    private int _gamePoints;
    public int GamePoints
    {
        get { return _gamePoints; }
        set { _gamePoints = value; }
    }
    private void Start()
    {
        platform = transform.Find("Platform").GetComponentsInChildren<PlatformBlock>();
        kBlock = platform.Length;
    }

    public void NextPlatform()
    {
        platform[GamePoints % kBlock].transform.position = new Vector3(0, kBlock + GamePoints, kBlock + GamePoints);
        GamePoints++;
    }
}
