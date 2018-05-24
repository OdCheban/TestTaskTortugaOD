using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagementGame : MonoBehaviour {
    PlatformBlock[] platform;
    private int kBlock;//platform.length
    private int _nextBlockN;
    private int _gamePoints;
    Text textPoints;
    public int NextBlockN // тоже самое что и  GamePoints % kBlock
    {
        get { return _nextBlockN; }
        set {
            if (value >= kBlock)
                value = 0;
            _nextBlockN = value;
        }
    }
    public int GamePoints
    {
        get { return _gamePoints; }
        set {
            _gamePoints = value;
            textPoints.text = value.ToString();
        }
    }
    private void Start()
    {
        textPoints = GameObject.Find("Canvas/PointsText").GetComponent<Text>();
        platform = transform.Find("PlatformObjects").GetComponentsInChildren<PlatformBlock>();
        kBlock = platform.Length;
    }
    
    public int GetCoordLastPos()
    {
        return kBlock + GamePoints;
    }

    public void NextPlatform()
    {
        platform[NextBlockN].transform.position = new Vector3(0, GetCoordLastPos(), GetCoordLastPos());
        NextBlockN++;
        GamePoints++;
    }
}
