using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace gameDream
{
    public class ManagementGame : MonoBehaviour, TimeControl
    {
        EnemyGenerations eg;
        PlatformBlock[] platform;
        private int _nextBlockN;
        private int _gamePoints;
        Text textPoints;

        //статичные препятствия
        List<string> pathEnemyStatic = new List<string>();
        List<StaticEnemy> stEnemyList = new List<StaticEnemy>();
        [SerializeField][Range(0, 100)]
        int chanceObstacle;
        bool lastCreate;//2 прептятвия подряд это плохо.

        List<string> pathBonus = new List<string>();
        [SerializeField]
        [Range(0, 100)]
        int chanceBonus;

        float timeStop;

        public int NextBlockN // тоже самое что и  GamePoints % kBlock
        {
            get { return _nextBlockN; }
            set {
                if (value >= platform.Length)
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
            GetPathFiles();
            textPoints = GameObject.Find("Canvas/PointsText").GetComponent<Text>();
            platform = transform.Find("PlatformObjects").GetComponentsInChildren<PlatformBlock>();
            eg = GetComponent<EnemyGenerations>();
        }

        void GetPathFiles()
        {
            pathEnemyStatic = GetFileNames("Assets/Resources/EnemyStatic", "EnemyStatic");
            pathBonus = GetFileNames("Assets/Resources/Bonus", "Bonus");
        }
        List<string> GetFileNames(string pathToFolder,string endFolder)//или если проще = вручную в инспекторе или в файле прописать имена всех врагов.
        {
            List<string> pathList = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(pathToFolder);
            FileInfo[] info = dir.GetFiles("*.prefab");
            foreach (FileInfo f in info)
            {
                string path = f.FullName;
                path = path.Remove(0, path.IndexOf(endFolder));
                path = path.Replace(".prefab", "");
                pathList.Add(path);
            }
            return pathList;
        }

        public int GetCoordLastPos()
        {
            return platform.Length + GamePoints;
        }

        void CreateObstacle()
        {
            GameObject enemyStatic = (GameObject)Instantiate(Resources.Load(pathEnemyStatic[Random.Range(0, pathEnemyStatic.Count)]));
            enemyStatic.transform.position = platform[NextBlockN].transform.position;
            enemyStatic.transform.SetParent(platform[NextBlockN].transform);
            stEnemyList.Add(enemyStatic.GetComponent<StaticEnemy>());
            enemyStatic.GetComponent<StaticEnemy>().Activate();////?? StopEnemy faster Start ??

            if (timeStop > 0)
                enemyStatic.GetComponent<StaticEnemy>().StopEnemy(timeStop);
        }
        void CreateBonus()
        {
            GameObject bonusObj = (GameObject)Instantiate(Resources.Load(pathBonus[Random.Range(0, pathBonus.Count)])) as GameObject;
            bonusObj.transform.position = new Vector3(Random.Range(eg.bordersSpawnX.x, eg.bordersSpawnX.y), platform[NextBlockN].transform.position.y + 1.0f, platform[NextBlockN].transform.position.z);
            bonusObj.transform.SetParent(platform[NextBlockN].transform);
        }

        void SpawnObstacle()
        {
            int chance = Random.Range(0, 100);
            if (chance < chanceObstacle && !lastCreate)//шанс появления препятствия 
            {
                lastCreate = true;
                CreateObstacle();
            }
            else
                lastCreate = false;
        }
        void SpawnBonus()
        {
            int chance = Random.Range(0, 100);
            if (chance < chanceBonus)//шанс появления препятствия 
            {
                CreateBonus();
            }
        }

        public void NextPlatform()
        {
            platform[NextBlockN].transform.position = new Vector3(0, GetCoordLastPos(), GetCoordLastPos());
            platform[NextBlockN].ClearObstacle();//очищаем от прошлых препятствий(если такие есть)
            SpawnObstacle();
            SpawnBonus();
            NextBlockN++;
            GamePoints++;
        }

        void Update()
        {
            if (timeStop > 0)//Invoke does not fit
                timeStop -= Time.deltaTime;
            else if (timeStop <= 0 && timeStop > -1)
            {
                СontinueEnemy();
                timeStop = -1;
            }
        }

        public void StopEnemy(float sec)
        {
            timeStop = sec;
            for (int i = 0; i < stEnemyList.Count; i++)
                if (stEnemyList[i])
                    stEnemyList[i].StopEnemy(sec);
            eg.StopEnemy(sec);
        }

        public void СontinueEnemy()
        {
            timeStop = 0;
        }
    }
}