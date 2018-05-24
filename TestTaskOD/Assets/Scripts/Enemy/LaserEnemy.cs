using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour {
    [SerializeField]
    float interval;
    GameObject laser;
    bool on;
	void Start () {
        laser = transform.Find("Laser").gameObject;
        StartCoroutine(LaserOn());
    }

    private IEnumerator LaserOn()
    {
        while (true)
        {
            on = !on;
            laser.SetActive(on);
            yield return new WaitForSeconds(interval);
        }
    }
}
