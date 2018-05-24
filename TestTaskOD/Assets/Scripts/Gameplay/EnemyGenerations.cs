using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyGenerations : MonoBehaviour {
    ManagementGame mg;
    public Vector2 bordersSpawnX;
    List<string> namesEnemy = new List<string>();
    [SerializeField]
    float intervalSpawn;

    void GetEnemeyFileNames()//или если проще = вручную в инспекторе или в файле прописать имена всех врагов.
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Enemy");
        FileInfo[] info = dir.GetFiles("*.prefab");
        foreach (FileInfo f in info)
        {
            namesEnemy.Add(f.Name.Replace(".prefab",""));
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
        if (obj.name == "EnemyCylinder(Clone)")
            enemyScale = obj.GetComponent<CapsuleCollider>().height / 2;
        float xCoord = Random.Range(bordersSpawnX.x, bordersSpawnX.y);
        if (obj.name == "CubeEnemy(Clone)" && xCoord > 3)
            xCoord = 2.8f;
        return new Vector3(xCoord, mg.GetCoordLastPos() - ((1 - enemyScale) / 2), mg.GetCoordLastPos() - 1 + ((1 - enemyScale) / 2));
    }
    private IEnumerator CreateEnemy()
    {
        while (true)
        {
            GameObject enemy = (GameObject)Instantiate(Resources.Load("Enemy/"+namesEnemy[Random.Range(0,namesEnemy.Count)])) as GameObject;
            enemy.transform.position = GetSpawnPosition(enemy);
            yield return new WaitForSeconds(Random.Range(1, intervalSpawn));
        }
    }
}
