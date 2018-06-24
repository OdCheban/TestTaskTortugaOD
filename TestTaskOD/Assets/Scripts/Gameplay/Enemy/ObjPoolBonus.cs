using UnityEngine;


namespace gameDream
{
    public class ObjPoolBonus :  PoolStaticObject
    {
        protected override void InitPool()
        {
            tagObj = "EnemyStatic";
        }
        protected override Vector3 PosObj(Transform platform)
        {
            return new Vector3(Random.Range(AllFunc.spawnXmin, AllFunc.spawnXmax), platform.position.y + 1.25f, platform.position.z);
        }
    }
}
