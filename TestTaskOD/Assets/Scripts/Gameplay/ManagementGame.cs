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
        [SerializeField]
        PlatformBlock[] platform;
        private int _nextBlockN;
        private int _gamePoints;
        Text textPoints;
        Transform fallTrigger;

        string[] pathEnemyStatic;
        string[] pathBonus;

        //статичные препятствия
        List<StaticEnemy> stEnemyList = new List<StaticEnemy>();
        [SerializeField][Range(0, 100)]
        int chanceObstacle;
        bool lastCreate;//2 прептятвия подряд это плохо.

        [SerializeField]
        [Range(0, 100)]
        int chanceBonus;

        float timeStop;//bonus_time

        GameObject insideRenderWorld;
        GameObject outsideRenderWorld;

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
                if(value == 200)
                {
                    insideRenderWorld.SetActive(false);
                    outsideRenderWorld.SetActive(true);
                }
            }
        }

        void RenderWorld()
        {
            insideRenderWorld = GameObject.Find("World/InsidePlanet").gameObject;
            outsideRenderWorld = GameObject.Find("World/OutsidePlanet").gameObject;
            outsideRenderWorld.SetActive(false);
        }

        //void GenerWorld()
        //{
        //      instianiate(block)
        //    for(int i = 0; i < platform.Length;i++)
        //        platform[i].transform.position = new Vector3(0, AllFunc.scaleBlock * i, i*AllFunc.scaleBlock);
        //    GameObject.Find("RollerBall").transform.position = new Vector3(0, AllFunc.scaleBlock * 6, AllFunc.scaleBlock * 5+0.05f);//старт на 6 блоке
        // 
        //}

        private void Start()
        {
            //GenerWorld();
            RenderWorld();

            pathEnemyStatic = AllFunc.GetPathEnemyStatic();
            pathBonus = AllFunc.GetPathBonus();

            fallTrigger = transform.Find("FallTrigger");
            textPoints = GameObject.Find("Canvas/PointsText").GetComponent<Text>();
            eg = GetComponent<EnemyGenerations>();
        }

        public float GetCoordLastPos()
        {
            return (platform.Length + GamePoints) * AllFunc.scaleBlock;
        }

        void CreateObstacle()
        {
            GameObject enemyStatic = (GameObject)Instantiate(Resources.Load(pathEnemyStatic[Random.Range(0, pathEnemyStatic.Length)]));
            enemyStatic.transform.position = platform[NextBlockN].transform.position;
            enemyStatic.transform.SetParent(platform[NextBlockN].transform);
            stEnemyList.Add(enemyStatic.GetComponent<StaticEnemy>());
            enemyStatic.GetComponent<StaticEnemy>().Activate();////?? StopEnemy faster Start ??

            if (timeStop > 0)
                enemyStatic.GetComponent<StaticEnemy>().StopEnemy(timeStop);
        }
        void CreateBonus()
        {
            GameObject bonusObj = (GameObject)Instantiate(Resources.Load(pathBonus[Random.Range(0, pathBonus.Length)])) as GameObject;
            bonusObj.transform.position = new Vector3(Random.Range(AllFunc.spawnXmin, AllFunc.spawnXmax), platform[NextBlockN].transform.position.y + 1.0f, platform[NextBlockN].transform.position.z);
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
        
        void RefreshPosFallObj()
        {
            fallTrigger.position = platform[NextBlockN].transform.position;
        }
        public void NextPlatform()
        {
            platform[NextBlockN].transform.position = new Vector3(0, GetCoordLastPos(), GetCoordLastPos());
            platform[NextBlockN].ClearObstacle();//очищаем от прошлых препятствий(если такие есть)
            SpawnObstacle();
            SpawnBonus();
            NextBlockN++;
            RefreshPosFallObj();
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