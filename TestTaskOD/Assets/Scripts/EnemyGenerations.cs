using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerations : MonoBehaviour {
    ManagementGame mg;
    void Start()
    {
        mg = GetComponent<ManagementGame>();
        StartCoroutine(CreateEnemy(1));
    }

    private IEnumerator CreateEnemy(float waitTime)
    {
        while (true)
        {
            GameObject shell = (GameObject)Instantiate(Resources.Load("EnemySphere")) as GameObject;
            shell.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), mg.GetCoordLastPos(), mg.GetCoordLastPos()-1);
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }
}
