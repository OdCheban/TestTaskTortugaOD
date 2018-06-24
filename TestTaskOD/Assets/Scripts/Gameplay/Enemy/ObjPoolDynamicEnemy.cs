using System.Collections;
using UnityEngine;

namespace gameDream
{
    public class ObjPoolDynamicEnemy : ObjPoolEnemy, TimeControl
    {
        public float spawnTime;
        public float timeLife;
        float timeStop;

        protected override void InitPool()
        {
            StartCoroutine(CreateEnemy());
        }

        private void Update()
        {
            if (timeStop <= 0)
            {
                for (int i = enemies.Count - 1; i >= 0; i--)
                    if (!enemies[i].GetComponent<DynamicEnemy>().Action())
                        DeActivateObject(enemies[i]);
            }
            else
            {
                timeStop -= Time.deltaTime;
            }
        }
        private IEnumerator CreateEnemy()
        {
            while (true)
            {
                if (timeStop <= 0)
                {
                    GameObject newEnemy = PoolObject();

                    newEnemy.transform.position = GetSpawnPosition(newEnemy.gameObject);
                    newEnemy.GetComponent<DynamicEnemy>().timerLive = timeLife;
                }
                yield return new WaitForSeconds(Random.Range(1, spawnTime));
            }
        }

        Vector3 GetSpawnPosition(GameObject obj)
        {
            float enemyScale = obj.transform.localScale.z;
            if (obj.name == "EnemyCylinder(Clone)") //радиус(y) у каждого объекта разный 
                enemyScale = obj.GetComponent<CapsuleCollider>().height / 2;
            float xCoord = Random.Range(AllFunc.spawnXmin, AllFunc.spawnXmax);
            return new Vector3(xCoord, mg.GetCoordLastPos() - ((1 - enemyScale) / 2), mg.GetCoordLastPos() - 1 + ((1 - enemyScale) / 2));
        }

        public void Stop(float time)
        {
            timeStop = time;
        }
    }
}