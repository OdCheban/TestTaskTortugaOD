using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ManagementGame : MonoBehaviour {
    PlatformBlock[] platform;
    private int kBlock;//platform.length
    private int _nextBlockN;
    private int _gamePoints;
    Text textPoints;

    //статичные препятствия
    List<string> pathEnemyStatic = new List<string>();
    [SerializeField] [Range(0,100)]
    int chanceObstacle;
    bool lastCreate;//2 прептятвия подряд это плохо.

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
        GetEnemeyFileNames();
        textPoints = GameObject.Find("Canvas/PointsText").GetComponent<Text>();
        platform = transform.Find("PlatformObjects").GetComponentsInChildren<PlatformBlock>();
        kBlock = platform.Length;
    }
    void GetEnemeyFileNames()//или если проще = вручную в инспекторе или в файле прописать имена всех врагов.
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/EnemyStatic");
        FileInfo[] info = dir.GetFiles("*.prefab");
        foreach (FileInfo f in info)
        {
            string path = f.FullName;
            path = path.Remove(0, path.IndexOf("EnemyStatic"));
            path = path.Replace(".prefab", "");
            pathEnemyStatic.Add(path);
        }
    }
    public int GetCoordLastPos()
    {
        return kBlock + GamePoints;
    }

    void SpawnObstacle()
    {
        if (Random.Range(0, 100) < chanceObstacle && !lastCreate)//шанс появления препятствия 
        {
            lastCreate = true;
            GameObject enemyStatic = (GameObject)Instantiate(Resources.Load(pathEnemyStatic[Random.Range(0, pathEnemyStatic.Count)])) as GameObject;
            enemyStatic.transform.position = platform[NextBlockN].transform.position;
            enemyStatic.transform.SetParent(platform[NextBlockN].transform);
        }
        else
        {
            lastCreate = false;
        }
    }

    public void NextPlatform()
    {
        platform[NextBlockN].transform.position = new Vector3(0, GetCoordLastPos(), GetCoordLastPos());
        platform[NextBlockN].ClearObstacle();//очищаем от прошлых препятствий(если такие есть)
        SpawnObstacle();
        NextBlockN++;
        GamePoints++;
    }
}
