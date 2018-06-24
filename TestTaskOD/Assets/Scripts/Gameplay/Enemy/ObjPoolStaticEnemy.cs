using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gameDream
{
    public class ObjPoolStaticEnemy :  PoolStaticObject,TimeControl
    {

        float timeStop = -1;

        protected override void InitPool()
        {
            tagObj = "EnemyStatic";
        }

        protected override Vector3 PosObj(Transform platform)
        {
            return platform.position;
        }

        protected override void FinishCreateObj(GameObject obj)
        {
            obj.GetComponent<StaticEnemy>().Activate();
        }

        private void Update()
        {
            if (timeStop <= 0 && timeStop != -1)
            {
                foreach (GameObject enemy in enemies)
                    enemy.GetComponent<StaticEnemy>().ActivateItemKill();
                timeStop = -1;
            }
            else if (timeStop > 0)
            {
                timeStop -= Time.deltaTime;
            }
        }

        public void Stop(float time)
        {
            timeStop = time;
            foreach (GameObject enemy in enemies)
                enemy.GetComponent<StaticEnemy>().DeactivateItemKill();
        }
    }
}