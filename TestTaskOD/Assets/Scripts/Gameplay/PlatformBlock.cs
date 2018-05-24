using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBlock : MonoBehaviour {
    
    public void ClearObstacle()
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
    }
}
