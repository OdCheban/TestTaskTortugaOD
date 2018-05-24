using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBlock : MonoBehaviour {
    
    public void ClearObstacle()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}
