using UnityEngine;
using UnityEngine.UI;

namespace gameDream
{
    public class ManagementGame : MonoBehaviour, TimeControl
    {
        [SerializeField]
        Transform[] platform;
        private int _nextBlockN;
        private int _gamePoints;
        Text textPoints;
        Transform fallTrigger;

        GameObject insideRenderWorld;
        GameObject outsideRenderWorld;

        ObjPoolStaticEnemy poolStaticEnemy;
        ObjPoolDynamicEnemy poolDynamicEnemy;
        ObjPoolBonus poolBonus;

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

        private void Start()
        {
            RenderWorld();
            fallTrigger = transform.Find("FallTrigger");
            textPoints = GameObject.Find("Canvas/PointsText").GetComponent<Text>();
            GetPool();
        }

        void GetPool()
        {
            poolBonus = GetComponent<ObjPoolBonus>();
            poolStaticEnemy = GetComponent<ObjPoolStaticEnemy>();
            poolDynamicEnemy = GetComponent<ObjPoolDynamicEnemy>();
        }

        public float GetCoordLastPos()
        {
            return (platform.Length + GamePoints) * AllFunc.scaleBlock;
        }
        
        void RefreshPosFallObj()
        {
            fallTrigger.position = platform[NextBlockN].position;
        }
        public void NextPlatform()
        {
            platform[NextBlockN].transform.position = new Vector3(0, GetCoordLastPos(), GetCoordLastPos());
            poolStaticEnemy.ClearObstacle(platform[NextBlockN]);//очищаем от прошлых препятствий(если такие есть)
            poolStaticEnemy.SpawnObject(platform[NextBlockN]);
            poolBonus.ClearObstacle(platform[NextBlockN]);
            poolBonus.SpawnObject(platform[NextBlockN]);
            NextBlockN++;
            RefreshPosFallObj();
            GamePoints++;
        }

        public void Stop(float time)
        {
            poolStaticEnemy.Stop(time);
            poolDynamicEnemy.Stop(time);
        }
    }
}