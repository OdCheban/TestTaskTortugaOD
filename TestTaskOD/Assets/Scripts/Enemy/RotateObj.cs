using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour {
    public float speed;
	void Update () {
        transform.Rotate(Vector3.down, speed * Time.deltaTime);
    }
}
