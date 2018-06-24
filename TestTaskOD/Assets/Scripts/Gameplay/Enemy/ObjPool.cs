using System.Collections.Generic;
using UnityEngine;
namespace gameDream
{
    public class ObjPool : MonoBehaviour
    {
        public GameObject prefab;
        Stack<GameObject> enemies;
        public ObjPool(GameObject prefab)
        {
            this.prefab = prefab;
            enemies = new Stack<GameObject>();
        }
        public void Push(GameObject enemy)
        {
            enemy.gameObject.SetActive(false);
            enemies.Push(enemy);
        }
        public GameObject Pop()
        {
            if (enemies.Count == 0)
            {
                GameObject temp = Instantiate(prefab) as GameObject;
                return temp;
            }
            else
            {
                GameObject temp = enemies.Pop();
                temp.gameObject.SetActive(true);
                return temp;
            }
        }
    }
}