using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace gameDream
{
    public class EnemyGenerations : MonoBehaviour,TimeControl
    {
        ManagementGame mg;
        public Vector2 bordersSpawnX;
        List<string> pathEnemyDynamic = new List<string>();
        List<DynamicEnemy> dynamicEnemy = new List<DynamicEnemy>();
        [SerializeField] float intervalSpawn;
        private GameObject fatherEnemy;
        private float timeStop;

        void GetEnemeyFileNames()//или если проще = вручную в инспекторе или в файле прописать имена всех врагов.
        {
            DirectoryInfo dir = new DirectoryInfo("Assets/Resources/EnemyDynamic");
            FileInfo[] info = dir.GetFiles("*.prefab");
            foreach (FileInfo f in info)
            {
                string path = f.FullName;
                path = path.Remove(0, path.IndexOf("EnemyDynamic"));
                path = path.Replace(".prefab", "");
                pathEnemyDynamic.Add(path);
            }
        }

        void Start()
        {
            GetEnemeyFileNames();
            mg = GetComponent<ManagementGame>();

            fatherEnemy = new GameObject("FatherEnemy");
            StartCoroutine(CreateEnemy());
        }

        Vector3 GetSpawnPosition(GameObject obj)
        {
            float enemyScale = obj.transform.localScale.z;
            if (obj.name == "EnemyCylinder(Clone)") //радиус(y) у каждого объекта разный 
                enemyScale = obj.GetComponent<CapsuleCollider>().height / 2;
            float xCoord = Random.Range(bordersSpawnX.x, bordersSpawnX.y);
            if (obj.name == "CubeEnemy(Clone)" && xCoord > bordersSpawnX.y - 2)//2 - далность полета по x у кубика
                xCoord = 2.8f;
            return new Vector3(xCoord, mg.GetCoordLastPos() - ((1 - enemyScale) / 2), mg.GetCoordLastPos() - 1 + ((1 - enemyScale) / 2));
        }

        private IEnumerator CreateEnemy()
        {
            while (true)
            {
                GameObject enemy = (GameObject)Instantiate(Resources.Load(pathEnemyDynamic[Random.Range(0, pathEnemyDynamic.Count)]));
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
                de.StopEnemy(sec);
        }

        public void СontinueEnemy()
        {
            
        }
    }
}