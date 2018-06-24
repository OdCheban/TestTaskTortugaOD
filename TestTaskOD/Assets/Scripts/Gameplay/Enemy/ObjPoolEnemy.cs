using System.Collections.Generic;
using UnityEngine;


namespace gameDream
{
    public class ObjPoolEnemy : MonoBehaviour
    {
        protected ManagementGame mg;
        public GameObject[] prefab;
        protected List<GameObject> enemies = new List<GameObject>();
        protected List<ObjPool> pool = new List<ObjPool>();

        void Start()
        {
            mg = GetComponent<ManagementGame>();
            foreach (GameObject obj in prefab)
                pool.Add(new ObjPool(obj));
            InitPool();
        }
        protected virtual void InitPool() { }
        protected GameObject PoolObject()
        {
            GameObject enemyStatic = pool[Random.Range(0, pool.Count)].Pop();
            enemies.Add(enemyStatic);
            return enemyStatic;
        }
        protected void DeActivateObject(GameObject obj)
        {
            for (int q = 0; q < pool.Count; q++)
                if (obj.name == pool[q].prefab.name + "(Clone)")
                {
                    pool[q].Push(obj);
                    enemies.Remove(obj);
                    break;
                }
        }
    }

    public class PoolStaticObject : ObjPoolEnemy
    {
        [SerializeField]
        [Range(0, 100)]
        int chanceSpawn;
        protected string tagObj;

        public void SpawnObject(Transform platform)
        {
            int chance = Random.Range(0, 100);
            if (chance < chanceSpawn)
            {
                CreateObject(platform);
            }
        }
        void CreateObject(Transform platform)
        {
            GameObject obj = PoolObject();
            obj.transform.position = PosObj(platform);
            obj.transform.SetParent(platform);
            FinishCreateObj(obj);
        }
        protected virtual void FinishCreateObj(GameObject obj){ }

        protected virtual Vector3 PosObj(Transform platform)
        {
            return Vector3.zero;
        }

        public void ClearObstacle(Transform platform)
        {
            for (int i = 0; i < platform.childCount; i++)
            {
                GameObject child = platform.GetChild(i).gameObject;
                if (child.tag == tagObj)
                    DeActivateObject(child);
            }
        }
    }
}
