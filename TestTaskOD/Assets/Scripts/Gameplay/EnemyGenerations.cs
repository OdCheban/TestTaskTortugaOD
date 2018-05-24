using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyGenerations : MonoBehaviour {
    ManagementGame mg;
    public Vector2 bordersSpawnX;
    List<string> pathEnemyDynamic = new List<string>();
    [SerializeField]
    float intervalSpawn;

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
            GameObject enemy = (GameObject)Instantiate(Resources.Load(pathEnemyDynamic[Random.Range(0,pathEnemyDynamic.Count)])) as GameObject;
            enemy.transform.position = GetSpawnPosition(enemy);
            yield return new WaitForSeconds(Random.Range(1, intervalSpawn));
        }
    }
}
