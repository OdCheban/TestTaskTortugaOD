using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace gameDream
{
    public class EnemyGenerations : MonoBehaviour,TimeControl
    {
        ManagementGame mg;
        string[] pathEnemyDynamic;
        List<DynamicEnemy> dynamicEnemy = new List<DynamicEnemy>();
        [SerializeField] float intervalSpawn;
        private GameObject fatherEnemy;
        private float timeStop;


        void Start()
        {
            pathEnemyDynamic = AllFunc.GetPathEnemyDynamic();
            mg = GetComponent<ManagementGame>();

            fatherEnemy = new GameObject("FatherEnemy");
            StartCoroutine(CreateEnemy());
        }

        Vector3 GetSpawnPosition(GameObject obj)
        {
            float enemyScale = obj.transform.localScale.z;
            if (obj.name == "EnemyCylinder(Clone)") //радиус(y) у каждого объекта разный 
                enemyScale = obj.GetComponent<CapsuleCollider>().height / 2;
            float xCoord = Random.Range(AllFunc.spawnXmin, AllFunc.spawnXmax);
            return new Vector3(xCoord, mg.GetCoordLastPos() - ((1 - enemyScale) / 2), mg.GetCoordLastPos() - 1 + ((1 - enemyScale) / 2));
        }

        private IEnumerator CreateEnemy()
        {
            while (true)
            {
                GameObject enemy = (GameObject)Instantiate(Resources.Load(pathEnemyDynamic[Random.Range(0, pathEnemyDynamic.Length)]));
                enemy.transform.position = GetSpawnPosition(enemy);
                enemy.transform.SetParent(fatherEnemy.transform);
                dynamicEnemy.Add(enemy.GetComponent<DynamicEnemy>());
                if (timeStop > 0)
                    enemy.GetComponent<DynamicEnemy>().StopEnemy(timeStop);
                yield return new WaitForSeconds(Random.Range(1, intervalSpawn));
            }
        }

        void Update()
        {
            if (timeStop > 0) timeStop -= Time.deltaTime;
        }

        public void StopEnemy(float sec)
        {
            timeStop = sec;
            foreach (DynamicEnemy de in dynamicEnemy)
                if(de)
                    de.StopEnemy(sec);
        }

        public void СontinueEnemy()
        {
            
        }
    }
}