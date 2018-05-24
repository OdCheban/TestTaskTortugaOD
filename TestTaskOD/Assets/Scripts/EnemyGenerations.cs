using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerations : MonoBehaviour {

    void Start()
    {
        StartCoroutine(CreateEnemy(2.0f));
    }

    private IEnumerator CreateEnemy(float waitTime)
    {
        while (true)
        {
            GameObject shell = (GameObject)Instantiate(Resources.Load("EnemyCylinder")) as GameObject;
            shell.transform.position = new Vector3(Random.Range(-4.5f,4.5f), 5, 5);
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }
}
